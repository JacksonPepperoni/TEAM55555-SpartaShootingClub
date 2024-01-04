using UnityEngine;

public class ThrowWeapon : Projectile
{
    private float _speed = 4000;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponentInChildren<Rigidbody>();
        _rigidbody.AddForce(transform.forward * _speed);
    }

}
