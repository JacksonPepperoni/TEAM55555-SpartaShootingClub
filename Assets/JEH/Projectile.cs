using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionsRadius * 0.5f);
    }


    private ParticleSystem _particleSystem;
    private float _damage = 100;

    [SerializeField] private float _explosionsRadius; // 범위가 몇유닛인지적어주세요
    [SerializeField] private float _power;
    [SerializeField] private GameObject TestPrefab;

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

 
    void Explosion()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionsRadius * 0.5f, _layerMask);



        for (int i = 0; i < colliders.Length; i++)
        {
            Debug.Log(colliders[i].gameObject.name + DamageCalculation(colliders[i].gameObject.transform.position, _damage, _explosionsRadius));

            Rigidbody rig = colliders[i].GetComponent<Rigidbody>();

            if (rig != null)
                rig.AddExplosionForce(_power, transform.position - rig.gameObject.transform.position, _explosionsRadius * 7);

        }


        // 폭탄 이펙트 소환
        Instantiate(TestPrefab, transform.position, Quaternion.identity);

        ReturnPool();
    }



    protected virtual void ReturnPool()
    {
        CancelInvoke();
        Destroy(this.gameObject);
    }
    protected virtual float DamageCalculation(Vector3 target, float damage, float range)
    {
        float per = (Vector3.Distance(transform.position, target) / range * 100); // 피격위치가 사정거리의 몇퍼센트인지
        return damage * (1 - per / 100); // 떨어진 거리만큼 데미지 감소
    }
}
