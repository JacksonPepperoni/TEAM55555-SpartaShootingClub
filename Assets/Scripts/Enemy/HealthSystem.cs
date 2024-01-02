using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    private EnemyStatsHandler _statsHandler;

    public int maxHealth = 100;
    public float currentHealth { get; private set; }
    //public float maxHealth => _statsHandler.CurrentStats.maxHealth;
    private void Awake()
    {
        //_statsHandler = GetComponent<EnemyStatsHandler>();
    }

    private void Start()
    {
        //currentHealth = _statsHandler.CurrentStats.maxHealth;
        currentHealth = maxHealth;
    }

    public void OnDamage(float damage)
    {
        Debug.Log("�±� �� ������"+ currentHealth);
        currentHealth -= damage;
        Debug.Log("���� �� ������" + currentHealth);
        currentHealth = currentHealth < 0 ? 0 : currentHealth;

        if (currentHealth <= 0f)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Debug.Log("�׾���");
        Destroy(gameObject);

    }

}
