using System.Collections;
using UnityEngine;

public class MisteryBoxBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject TextObject;
    private IEnumerator Respawn()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        TextObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(4f);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        TextObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Respawn());
        }
    }
}
