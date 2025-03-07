using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControler : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]
    private Rigidbody _rb; // rigidbody du gameobject
    private float _speed, _accelerationLerpInterpolator, _speedPower, _rotationInput, _rotationFactor; //vitesse, stockage de l'acceleration au moment meme, input qui indique si le joueur veut tourner et dans quel sens, facteur de rotatation
    [SerializeField]
    private float _speedMax = 3, _accelerationFactor, _desaccelerationFactor, _rotationSpeed = 0.5f; //vitesse maximale, facteur d'acceleration, facteur de desacceleration, vitesse de rotation
    private bool _isAccelerating, _isSlowing, _canMove = true, _isCrazyRotating = false; //indique si le joueur est en train d'accelerer, indique si le joueur est en train de freiner, indique si le joueur peut bouger, indique si le joueur tourne du au stun

    [Header("Curves")]
    [SerializeField]
    private AnimationCurve _accelerationCurve; //courbe d'acceleration modifiee dans l'editeur
    [SerializeField]
    private AnimationCurve _powerSpeedCurve; //courbe de boost de vitesse modifiee dans l'editeur

    [Header("Relative to Prefabs")]
    public TrapBehavior TrapPrefab;
    public Transform SpawnOffset;

    [Header("Inventory")]
    private string _inventoryItem = "";
    private List<string> _inventory = new List<string>() {"Trap", "Boost" };
    private int _index;
    //PROF A VERIF
    private float _terrainSpeedVariator;
    [SerializeField]
    private LayerMask _layerMask;
    //
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //accelere
        {
            _isAccelerating = true;
        }
        if (Input.GetKeyUp(KeyCode.Space)) //arrete d'accelerer
        {
            _isAccelerating = false;
        }
        if (Input.GetKeyDown(KeyCode.Q) && _inventoryItem == "Boost") //recois un boost de vitesse si le joueur en a en stock
        {
            //_inventoryItem = "";
            SpeedPowerUp();
        }
        if (Input.GetKeyDown(KeyCode.E) && _inventoryItem == "Trap") //pose un piege si le joueur en a en stock
        {
            //_inventoryItem = "";
            SpawnTrap();
        }
        if (_canMove == true)
        {
            _rotationInput = Input.GetAxis("Horizontal"); //verifie si le joueur tourne
        }
        if (_isCrazyRotating == true) //tourne du a un stun
        {
            transform.eulerAngles += Vector3.down * _rotationSpeed * 2.4f * Time.deltaTime;
        }

        //PROF A VERIFIER ET BIEN COMPRENDRE
        if (Physics.Raycast(transform.position, transform.up * -1, out var info, 1, _layerMask))
        {

            Terrain terrainBellow = info.transform.GetComponent<Terrain>();
            if (terrainBellow != null)
            {
                _terrainSpeedVariator = terrainBellow.speedVariator;
            }
            else
            {
                _terrainSpeedVariator = 1;
            }
        }
        else
        {
            _terrainSpeedVariator = 1;
        }
        //
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow)) //freine
        {
            _isSlowing = true;
            _isAccelerating = false;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)) //arrete de freiner
        {
            _isSlowing = false;
            if (Input.GetKey(KeyCode.Space))
            {
                _isAccelerating = true;
            }
        }
        if (_isAccelerating && _canMove == true) //si le joueur accelere, augmente l'acceleration (sachant que cette augmentation ne peut pas depasser une certaine limite)
        {
            _accelerationLerpInterpolator += _accelerationFactor;
        }
        else //si le joueur n'accelere pas, diminue l'acceleration (sachant que cette augmentation ne peut pas depasser une certaine limite)
        {
            _accelerationLerpInterpolator -= _accelerationFactor * _desaccelerationFactor;
        }
        if (_isSlowing) //si le joueur freine, diminue l'acceleration
        {
            _accelerationLerpInterpolator -= _accelerationFactor * _desaccelerationFactor * 5;
        }

        _accelerationLerpInterpolator = Mathf.Clamp01(_accelerationLerpInterpolator); //clamp la valeur de l'acceleration entre 0 et 1

        _speedPower = Mathf.Clamp01(_speedPower); //clamp la valeur du boost de vitesse entre 0 et 1
        _speedPower -= 0.02f; //definit la vitesse de diminution du boost de vitesse

        if (_canMove == true)// verifie que le joueur peut bouger
        {
            _speed = _accelerationCurve.Evaluate(_accelerationLerpInterpolator) * _speedMax * (_powerSpeedCurve.Evaluate(_speedPower) + 1); //change la vitesse en fonction de l'acceleration et de sa courbe
        }

        _rotationFactor = _speed*_rotationSpeed / 3; //definis le facteur de rotation comme le produit de la vitesse et de la vitesse de rotation/3
        _rotationFactor = Mathf.Clamp(_rotationFactor, 20, 100); //clamp la valeur du facteur de rotation entre 20 et 100

        transform.eulerAngles += Vector3.up * _rotationInput * Time.deltaTime * _rotationFactor; //tourne (assurance que la camera ne tremble pas)
        _rb.MovePosition(transform.position + transform.forward * _speed * Time.fixedDeltaTime);
    }

    public void SpeedPowerUp() //fonction qui definit un boost de vitesse
    {
        _speedPower = 25;
    }

    public void SpawnTrap() //fonction fait apparaitre un piege
    {
        Instantiate(TrapPrefab, SpawnOffset.position, SpawnOffset.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap")) //si le joueur rencontre un piege, appelle la fonction correspondante
        {
            Destroy(other.gameObject);
            Stun();
        }
        if (other.CompareTag("Stun")) //si le joueur rencontre un element qui l'etourdit, appelle la fonction correspondante
        {
            Stun();
        }
        if (other.CompareTag("MisteryCube")) //si le joueur touche une boite mystère, appelle la fonction correspondante
        {
            Destroy(other.gameObject);
            ReceiveRandomItem();
        }
    }

    private void ReceiveRandomItem() //donne au joueur un item aleatoire
    {
        _index = UnityEngine.Random.Range(0, _inventory.Count);
        _inventoryItem = _inventory[_index];
    }

    private void Stun() //empeche le joueur de bouger
    {
        _canMove = false;
        _accelerationLerpInterpolator = 0;
        _speed = 0;
        _isCrazyRotating = true;
        StartCoroutine(CanMove());
    }

    private IEnumerator CanMove() //permet au joueur de bouger apres une duree definie
    {
        yield return new WaitForSeconds(1.5f);
        _canMove = true;
        _isCrazyRotating = false;
    }
}