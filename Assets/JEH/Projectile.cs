using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected ParticleSystem _particleSystem;
    protected float _damage;


    protected virtual void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    protected virtual void OnEnable()
    {
        ReturnPool();
    }


    // TODO 충돌체에 데미지주기


    protected virtual void ReturnPool()
    {
        if (_particleSystem != null)
            Destroy(this.gameObject, _particleSystem.duration);
        else
            Destroy(this.gameObject, 3f);

    }

}
