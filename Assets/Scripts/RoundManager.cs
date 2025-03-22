using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    private bool _checked = false;
    private int _rounds = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_rounds == 5) //nombre de tour a faire pour gagner
        {
            SceneManager.LoadScene(2);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StartingLine") && _checked == true) //si le joueur rencontre un element qui l'etourdit, appelle la fonction correspondante
        {
            _checked = false;
            _rounds += 1;
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
