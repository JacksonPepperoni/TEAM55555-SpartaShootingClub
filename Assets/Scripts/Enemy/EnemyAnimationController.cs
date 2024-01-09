using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController: MonoBehaviour
{
    private Animator animator;
    private EnemyShooting shotController;

    private static readonly int isActive = Animator.StringToHash("isActive");
    private static readonly int isReload = Animator.StringToHash("isReload");


    private void Awake()
    {
        animator=GetComponent<Animator>();
        shotController=GetComponent<EnemyShooting>();
    }
    public void SetAnimationActive(bool value)
    {
        animator.SetBool(isActive, value);
    }

    public void SetAnimationReload(bool value)
    {
        animator.SetBool(isReload, value);
    }

}
