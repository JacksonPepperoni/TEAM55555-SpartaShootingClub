using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    //private EnemyStatsHandler _statsHandler;

    public int maxHealth = 100;
    public float currentHealth { get; private set; }

    public event Action OnDeath;

    //[SerializeField] Animator _anim;

    private void Awake()
    {
        //_statsHandler = GetComponent<EnemyStatsHandler>();
        //_anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //currentHealth = _statsHandler.CurrentStats.maxHealth;
        InitHealthSystem();
    }

    public void InitHealthSystem()
    {
        currentHealth = maxHealth;
    }

    public void OnDamage(float damage)
    {
        Debug.Log("맞기 전 데미지"+ currentHealth);
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
        //_anim.SetBool("IsDie", true);
        //_anim.SetBool("IsActive", false);
        //gameObject.GetComponent<BoxCollider>().enabled = false;
        OnDeath?.Invoke();
        GetComponent<EnemyController>().Die();
    }
}
