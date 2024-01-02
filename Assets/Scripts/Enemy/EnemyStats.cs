using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    [Range(1, 100)] public int maxHealth;
    
    public AttackSO attackSO;
}
