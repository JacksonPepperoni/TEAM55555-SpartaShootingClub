using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Rendering;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool isActive = false;
    private float speed = 100f;

    IEnumerator SetActive()
    {
        while (true)
        {
            if (transform.rotation.x >= 0.0f)
            {
                isActive = true;
                break;
            }

            transform.Rotate(new Vector3(1, 0, 0) * Time.deltaTime * speed);
            yield return null;
        }
    }

    IEnumerator SetDeactive()
    {
        while (true)
        {
            if (transform.rotation.x <=-0.7f)
            {
                isActive = false;
                break;
            }

            transform.Rotate(new Vector3(-1, 0, 0) * Time.deltaTime * speed);
            yield return null;
        }
    }
    public void Respawn()
    {
        StartCoroutine("SetActive");
        gameObject.AddComponent<Rigidbody>().useGravity = false;
        gameObject.GetComponent<HealthSystem>().InitHealthSystem();
    }

    public void Die()
    {
        StartCoroutine("SetDeactive");
        Destroy(gameObject.GetComponent<Rigidbody>());
    }
}