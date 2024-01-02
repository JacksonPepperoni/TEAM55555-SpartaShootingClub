using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    static public EnemyManager instance;

    private void Awake()
    {
        instance = this;
    }
    
}
