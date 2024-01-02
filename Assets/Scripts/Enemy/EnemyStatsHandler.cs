using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsHandler : MonoBehaviour
{
    [SerializeField] private EnemyStats baseStats;
    public EnemyStats CurrentStats { get; private set; }

    private void Awake()
    {
        InitStats();
    }

    private void InitStats()
    {
        AttackSO _attackSO = null;
        if(baseStats.attackSO!=null)
        {
            _attackSO = Instantiate(baseStats.attackSO);
        }
        CurrentStats = new EnemyStats { attackSO = _attackSO };
    }

    private void UpdateStats(Func<float, float, float> operation , EnemyStats newModifier)
    {
        CurrentStats.maxHealth = (int)operation(CurrentStats.maxHealth, newModifier.maxHealth);

        if (CurrentStats.attackSO == null || newModifier.attackSO == null)
            return;
        UpdateAttackStats(operation, CurrentStats.attackSO, newModifier.attackSO);
    }

    private void UpdateAttackStats(Func<float, float, float> operation, AttackSO currentAttack, AttackSO newAttack)
    {
        if (currentAttack == null || newAttack == null)
        {
            return;
        }

        currentAttack.delay = operation(currentAttack.delay, newAttack.delay);
        currentAttack.power = operation(currentAttack.power, newAttack.power);
        currentAttack.size = operation(currentAttack.size, newAttack.size);
        currentAttack.speed = operation(currentAttack.speed, newAttack.speed);
    }
}


