using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController: MonoBehaviour
{
    private Animator animator;
    private EnemyShooting shotController;

    private static readonly int isAttack = Animator.StringToHash("isAttack");
    private static readonly int isReload = Animator.StringToHash("isReload");

    private void Awake()
    {
        animator=GetComponent<Animator>();
        shotController=GetComponent<EnemyShooting>();
    }


}
