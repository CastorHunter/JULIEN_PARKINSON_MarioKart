using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RacesManager : MonoBehaviour
{
    private GameObject _racesRuler;
    private RacesRulerScript _racesRulerScript;

    [SerializeField]
    private TextMeshProUGUI _redKartScore, _blueKartScore;
    [SerializeField]
    private GameObject _victory, _nextRace;

    void Start()
    {
        _racesRuler = GameObject.Find("RacesRuler");
        _racesRulerScript = _racesRuler.GetComponent<RacesRulerScript>();
        _blueKartScore.GetComponent<TextMeshProUGUI>().text = _racesRulerScript.blueVictories.ToString();
        _redKartScore.GetComponent<TextMeshProUGUI>().text = _racesRulerScript.redVictories.ToString();
        if (_racesRulerScript.redVictories == 2 || _racesRulerScript.blueVictories == 2)
        {
            _nextRace.SetActive(false);
            _victory.SetActive(true);
        }
        else
        {
            _victory.SetActive(false);
            _nextRace.SetActive(true);
        }
    }

    public void RestartRace()
    {
        SceneManager.LoadScene(1);
    }
}