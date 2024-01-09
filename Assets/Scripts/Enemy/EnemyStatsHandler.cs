using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStatsHandler : MonoBehaviour
{
    [SerializeField] private EnemyStats baseStats;
    public EnemyStats currentStats { get; private set; }

    private void Awake()
    {
        InitEnemyStats();
        
    }
    private void InitEnemyStats()
    {
        AttackSO attackSO = null;
        if (baseStats.attackSO != null)
        {
            attackSO = Instantiate(baseStats.attackSO);
        }
        currentStats = new EnemyStats { attackSO = attackSO };
        currentStats.speed= baseStats.speed;
        //if (_stats.attackSO != null)
        //{
        //    _stats.attackSO.target = baseStats.attackSO.target;
        //}
    }
}