using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class RoundManager : MonoBehaviour
{
    private bool _checked = false;
    public int rounds = 0;

    [SerializeField]
    private Image _roundUI;
    [SerializeField]
    private List<Sprite> _roundSprites = new List<Sprite>() { };

    [SerializeField]
    private CarControler _carControler;
    private GameObject _racesRuler;
    private RacesRulerScript _racesRulerScript;

    private void Start()
    {
        _racesRuler = GameObject.Find("RacesRuler");
        _racesRulerScript = _racesRuler.GetComponent<RacesRulerScript>();
    }
    void Update()
    {
        if (rounds == 6) //nombre de tour a faire pour gagner
        {
            _racesRulerScript.GiveVictory(gameObject);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StartingLine") && _checked == true) //si le joueur rencontre un element qui l'etourdit, appelle la fonction correspondante
        {
            _checked = false;
            rounds += 1;
            _carControler.roundScore = 0;
            if (rounds < 6)
            {
                _roundUI.sprite = _roundSprites[rounds];
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RoundChecker")) //si le joueur rencontre un piege, appelle la fonction correspondante
        {
            _checked = true;
        }
    }
}
