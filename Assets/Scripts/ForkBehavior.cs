using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkBehavior : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.position += transform.up * Time.deltaTime * 5;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CarControler _carControler = other.GetComponent<CarControler>();
            _carControler.Stun();
        }
        if (other.CompareTag("RoundCheckpoint") == false)
        {
            Destroy(gameObject);
        }
    }
}
