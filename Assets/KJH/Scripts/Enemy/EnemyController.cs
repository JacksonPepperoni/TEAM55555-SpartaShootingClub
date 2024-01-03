using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //protected EnemyStatsHandler _stats { get; private set; }

    //protected virtual void Awake()
    //{
    //    _stats = GetComponent<EnemyStatsHandler>();
    //}

    //지금 에니메이션 상태 확인해야되고
    //꺼져 있는지 켜져 있는지 확인해야 함.
    private Animator animator;
    private static readonly int IsActive = Animator.StringToHash("IsActive");
    private static readonly int IsDie = Animator.StringToHash("IsDie");

    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void Respawn()
    {
        Debug.Log("와");
        if (!animator.GetBool(IsActive))
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            gameObject.GetComponent<HealthSystem>().InitHealthSystem();
            animator.SetBool(IsActive, true);
            animator.SetBool(IsDie, false);
        }
    }

}
