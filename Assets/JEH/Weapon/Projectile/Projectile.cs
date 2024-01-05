using UnityEngine;
public class Projectile : MonoBehaviour
{
    #region Gizmo
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionsRadius * 0.5f);
    }

    #endregion

    [SerializeField] private float _damage;
    [SerializeField] private float _explosionsRadius; 
    [SerializeField] private float _force;
    [SerializeField] private GameObject _effectParticle;
    [SerializeField] private float _timeToDie; //몇초후에 터질지

    private int _layerMask;

    // LineRenderer

    private void OnEnable()
    {
        _layerMask = 1 << LayerMask.NameToLayer("Water");

        Invoke("Explosion", _timeToDie);
    }


    private void OnCollisionEnter(Collision collision) // 닿으면 터지는 폭탄용
    {
        //   Pow();
    }


    void Explosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionsRadius * 0.5f, _layerMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Debug.Log($"{colliders[i].gameObject.name}에게 {DamageCalculation(colliders[i].gameObject.transform.position, _damage, _explosionsRadius)}데미지!");
            Rigidbody rig = colliders[i].GetComponent<Rigidbody>();

            if (rig != null)
                rig?.AddExplosionForce(_force, transform.position - rig.gameObject.transform.position, _explosionsRadius * 7);

        }

        Instantiate(_effectParticle, transform.position, Quaternion.identity);
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
