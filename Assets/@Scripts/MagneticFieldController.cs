using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticFieldController : MonoBehaviour
{
    void Update()
    {
        transform.position += new Vector3(0, 0, 1) *3* Time.deltaTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("부딪침");
        }
    }
}
