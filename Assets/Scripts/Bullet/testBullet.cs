using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testBullet : MonoBehaviour
{
    int damage = 50;


    private void OnTriggerEnter(Collider collision)
    {
        HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
        if(healthSystem != null)
        {
            healthSystem.OnDamage(damage);
        }
        Destroy(gameObject); 
    }
}
