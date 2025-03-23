using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstWallScript : MonoBehaviour
{
    [SerializeField]
    private GameObject _target;

    void Update()
    {
        transform.localPosition = _target.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChickenDirectionBeacon"))
        {
            transform.eulerAngles = new Vector3 (0,0,0);
        }
        if (other.CompareTag("CheeseDirectionBeacon"))
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
    }
}
