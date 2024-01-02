using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected EnemyStatsHandler _stats { get; private set; }

    protected virtual void Awake()
    {
        _stats = GetComponent<EnemyStatsHandler>();
    }

}
