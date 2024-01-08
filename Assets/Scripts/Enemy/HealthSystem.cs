using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private int maxHealth = 10;
    public float currentHealth { get; private set; }

    private float timeSinceLastChange;
    private float hitDamageDelay;

    public event Action OnDeath;
    private void Start()
    {
        InitHealthSystem();
    }

    private void Update()
    {
        if(timeSinceLastChange < hitDamageDelay)
            timeSinceLastChange += Time.deltaTime;
    }

    public void InitHealthSystem()
    {
        currentHealth = maxHealth;
        timeSinceLastChange = float.MaxValue;
        hitDamageDelay = 0.5f;
    }

    public void HitDamage(float damage)
    {
        if(damage <=0 || timeSinceLastChange < hitDamageDelay)
        {
            return;
        }
        timeSinceLastChange = 0f;

        Debug.Log("맞기 전 데미지" + currentHealth);
        currentHealth -= damage;
        Debug.Log("맞은 후 데미지" + currentHealth);
        currentHealth = currentHealth < 0 ? 0 : currentHealth;

        if (currentHealth <= 0f)
        {
            CallDeath();
        }
    }

    private void CallDeath()
    {
        Debug.Log("죽었음");
        OnDeath?.Invoke();
        GetComponent<EnemyController>().Die();
    }
}