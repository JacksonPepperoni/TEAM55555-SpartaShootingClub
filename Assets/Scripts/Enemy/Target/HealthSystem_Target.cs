using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem_Target : HealthSystem
{
    protected override void CallDeath()
    {
        SetDeath();
        GetComponent<TargetController>().Die();
    }
}
