using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttackController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer; //지형 충돌 여부 확인용

    private RangedAttackData _attackData;
    private float _currentDuration;
    private Vector3 _direction;
    private bool _isReady;
    private int damage = 50;

    private Rigidbody _rigidbody;
    private SpriteRenderer _spriteRenderer;
    private TrailRenderer _trailRenderer;
    private ProjectileManager _projectileManager;

    public bool fxOnDestroy = true;
    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        //_trailRenderer = GetComponent<TrailRenderer>();
    }
    private void Update()//이동처리
    {
        if (!_isReady)
        {
            return;
        }
        _currentDuration += Time.deltaTime;
        if (_currentDuration > _attackData.duration)
        {
            DestroyProjectile(transform.position, false);
        }
        _rigidbody.velocity = -Vector3.right * 150 * Time.deltaTime; 
        //transform.position += new Vector3(0, 0, 1*_attackData.speed) * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if ((levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer))))
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - _direction * .2f, fxOnDestroy);
        }
        else if (_attackData.target.value == (_attackData.target.value | 1 << collision.gameObject.layer))
        {
            TakeDamage takeDamage = collision.GetComponent<TakeDamage>();
            if (takeDamage != null)
            {
                takeDamage.CallDamage(_attackData.power);
            }
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
        }
    }
    public void InitalizeAttack(Vector3 direction, RangedAttackData attackData, ProjectileManager projectileManager)//초기화
    {
        _projectileManager = projectileManager;
        _attackData = attackData;
        _direction = direction;

        UpdateProjectileSprite();
        //_trailRenderer.Clear();
        _currentDuration = 0;


        _isReady = true;
    }
    private void UpdateProjectileSprite()//초기화
    {
        transform.localScale = Vector3.one * _attackData.size;
    }
    private void DestroyProjectile(Vector3 position, bool createFx)//삭제
    {
        //if (createFx)
        //{
        //    _projectileManager.CreateImpactParticlelesAtPosition(position, _attackData);

        //}
        gameObject.SetActive(false);
    }

}
