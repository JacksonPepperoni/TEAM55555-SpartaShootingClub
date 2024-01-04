using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Projectile : MonoBehaviour
{
    protected ParticleSystem _particleSystem;
    protected float _damage;

  public  float _explosionsRadius; // 범위가 몇유닛인지적어주세요


    private int _layerMask;
    // LineRenderer

    protected virtual void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    protected virtual void OnEnable()
    {
        _layerMask = 1 << LayerMask.NameToLayer("Water");


        Invoke("Explosion", 3);

        //if (_particleSystem != null)
        //    Invoke("ReturnPool", _particleSystem.duration);
        //else
        //    Invoke("ReturnPool", 4);

    }


    private void OnCollisionEnter(Collision collision)
    {
     //   Pow();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionsRadius * 0.5f);
    }
    void Explosion()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionsRadius * 0.5f, _layerMask);



        for (int i = 0; i < colliders.Length; i++)
        {
            Debug.Log(colliders[i].gameObject.name);
        }

        // 폭탄 이펙트 소환


        ReturnPool();
    }



    protected virtual void ReturnPool()
    {
        CancelInvoke();
        Destroy(this.gameObject);
    }

}
