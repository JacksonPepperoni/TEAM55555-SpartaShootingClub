using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Rendering;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //지금 에니메이션 상태 확인해야되고
    //꺼져 있는지 켜져 있는지 확인해야 함.
    private Rigidbody _rigidbody;
    private Animator animator;
    private static readonly int IsActive = Animator.StringToHash("IsActive");
    private static readonly int IsDie = Animator.StringToHash("IsDie");

    public bool isActive = false;
    private float speed = 100f;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    IEnumerator SetActive()
    {
        while(true)
        {
            if (transform.rotation.x >= 0.0f)
            {
                isActive = true;
                break;
            }
                
            transform.Rotate(new Vector3(1, 0, 0) * Time.deltaTime * speed);
            yield return null;
        }
    }

    IEnumerator SetDeactive()
    {
        while (true)
        {
            if (transform.rotation.x <= -90.0f)
            {
                isActive = false;
                break;
            }
                
            transform.Rotate(new Vector3(-1, 0, 0) * Time.deltaTime * speed);
            yield return null;
        }
    }
    public void Respawn()
    {
        StartCoroutine("SetActive");
        gameObject.AddComponent<Rigidbody>().useGravity = false;
        //GetComponentInChildren<CapsuleCollider>().enabled = isActive;


        //if (!animator.GetBool(IsActive))
        //{
        //    //여기서 컴포넌트만 킬 수 있나 gameObject.GetComponent<BoxCollider>().enabled = true;
        //    gameObject.GetComponent<HealthSystem>().InitHealthSystem();
        //    animator.SetBool(IsActive, true);
        //    animator.SetBool(IsDie, false);
        //}
    }

    public void Die()
    {
        StartCoroutine("SetDeactive");
        Destroy(gameObject.GetComponent<Rigidbody>());
    }

}
