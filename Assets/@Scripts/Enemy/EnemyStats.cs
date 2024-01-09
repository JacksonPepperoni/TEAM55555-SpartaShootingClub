using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyStats
{
    [Range(0f,20f)] public float speed;
    
    public AttackSO attackSO;
}
