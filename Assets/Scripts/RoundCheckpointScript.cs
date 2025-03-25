using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundCheckpointScript : MonoBehaviour
{
    public int _index;

    [SerializeField]
    private RoundManager _redRoundManager, _blueRoundManager;
    [SerializeField]
    private CarControler _redCarControler, _blueCarController;
    [SerializeField]
    private Sprite _first, _second;
    [SerializeField]
    private Image _redPosition, _bluePosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CarControler _carControler = other.GetComponent<CarControler>();
            if (_carControler.roundScore == _index - 1)
            {
                _carControler.roundScore += 1;
                if(_redCarControler.roundScore > _blueCarController.roundScore && _redRoundManager.rounds >= _blueRoundManager.rounds)
                {
                    _redPosition.sprite = _first;
                    _bluePosition.sprite = _second;
                }
                if (_redCarControler.roundScore < _blueCarController.roundScore && _redRoundManager.rounds <= _blueRoundManager.rounds)
                {
                    _bluePosition.sprite = _first;
                    _redPosition.sprite = _second;
                }
            }
        }
    }
}
