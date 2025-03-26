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
    private Button _nextRace;
    [SerializeField]
    private Image _victory;

    void Start()
    {
        _racesRuler = GameObject.Find("RacesRuler");
        _racesRulerScript = _racesRuler.GetComponent<RacesRulerScript>();
        _blueKartScore.GetComponent<TextMeshProUGUI>().text = _racesRulerScript.blueVictories.ToString();
        _redKartScore.GetComponent<TextMeshProUGUI>().text = _racesRulerScript.redVictories.ToString();
        if (_racesRulerScript.redVictories == 2 || _racesRulerScript.blueVictories == 2)
        {
            _nextRace.enabled = false;
            _victory.enabled = true;
        }
        else
        {
            _victory.enabled = false;
            _nextRace.enabled = true;
        }
    }

    public void RestartRace()
    {
        SceneManager.LoadScene(1);
    }
}