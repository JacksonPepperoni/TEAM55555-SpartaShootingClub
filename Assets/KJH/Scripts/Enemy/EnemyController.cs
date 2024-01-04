using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //지금 에니메이션 상태 확인해야되고
    //꺼져 있는지 켜져 있는지 확인해야 함.
    private Animator animator;
    private static readonly int IsActive = Animator.StringToHash("IsActive");
    private static readonly int IsDie = Animator.StringToHash("IsDie");

    public bool isActive = false;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        GetComponentInChildren<CapsuleCollider>().enabled = isActive;
    }

    public void Respawn()
    {
        Debug.Log("와");
        isActive = true;
        GetComponentInChildren<CapsuleCollider>().enabled = isActive;
        while (transform.rotation.x >= 0)
        {
            transform.Rotate(new Vector3(1, 0, 0) * Time.deltaTime * 10f);
        }

        //if (!animator.GetBool(IsActive))
        //{
        //    //여기서 컴포넌트만 킬 수 있나 gameObject.GetComponent<BoxCollider>().enabled = true;
        //    gameObject.GetComponent<HealthSystem>().InitHealthSystem();
        //    animator.SetBool(IsActive, true);
        //    animator.SetBool(IsDie, false);
        //}
    }

}
