using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    private bool _checked = false;
    private int _rounds = 0;

    [SerializeField]
    private Image _roundUI;
    [SerializeField]
    private List<Sprite> _roundSprites = new List<Sprite>() { };

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
            if (_rounds < 5)
            {
                _roundUI.sprite = _roundSprites[_rounds];
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
