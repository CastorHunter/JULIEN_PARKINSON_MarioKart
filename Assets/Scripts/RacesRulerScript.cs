using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RacesRulerScript : MonoBehaviour
{
    public int redVictories, blueVictories;
    void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void GiveVictory(GameObject winner)
    {
        if(winner.name == "RedKart")
        {
            redVictories += 1;
            SceneManager.LoadScene(2);
        }
        if (winner.name == "BlueKart")
        {
            blueVictories += 1;
            SceneManager.LoadScene(2);
        }
    }
}
