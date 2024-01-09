using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    private bool isDie;
    public float currentHealth { get; private set; }


    public event Action OnDeath;
    private void Start()
    {
        InitHealthSystem();
    }

    public void InitHealthSystem()
    {
        currentHealth = maxHealth;
        isDie = false;
    }

    public virtual void HitDamage(float damage)
    {
        if(damage <=0 || isDie)
        {
            return;
        }
        currentHealth -= damage;
        currentHealth = currentHealth < 0 ? 0 : currentHealth;

        if (currentHealth <= 0f)
        {
            CallDeath();
        }
    }

    protected virtual void CallDeath()
    {
        SetDeath();
        GetComponent<EnemyController>().Die();
    }

    protected void SetDeath()
    {
        isDie = true;
        OnDeath?.Invoke();
    }
}