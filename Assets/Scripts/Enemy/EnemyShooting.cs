using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    private ProjectileManager _projectileManager;

    private EnemyAnimationController _animController;

    [SerializeField] private Transform projectileSpawnPosition;
    private Vector3 _aimDirection = new Vector3(0,0,1);

    private EnemyStatsHandler _stats { get; set; }
    private AttackSO _attackSO;

    private float _timeSinceLastAttack = float.MaxValue;
    private float _timeSinceLastReload = 0;
    private bool isAttacking=false;
    public bool isActive = false;

    private void Start()
    {
        _animController = GetComponent<EnemyAnimationController>();
        _projectileManager = ProjectileManager.instance;
        _stats = GetComponent<EnemyStatsHandler>();
        _attackSO = _stats.currentStats.attackSO;
    }

    private void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        if (_stats.currentStats.attackSO==null)
            return;

        HandleReload();
        
        if (_timeSinceLastAttack <= _stats.currentStats.attackSO.delay)
        {
            _timeSinceLastAttack += Time.deltaTime;
  
        }
        if (isActive && isAttacking && _timeSinceLastAttack > _stats.currentStats.attackSO.delay)
        {
            _timeSinceLastAttack = 0;
            OnShoot();
        }
    }
    private void HandleReload()
    {
        _timeSinceLastReload+=Time.deltaTime;

        switch (isAttacking)
        {
            case true://발사 중일 때
                _animController.SetAnimationReload(false);

                if (_timeSinceLastReload > 5f)
                {
                    _timeSinceLastReload = 0;
                    isAttacking = false;
                }

                break;
            case false://재장전 중일 때
                _animController.SetAnimationReload(true);
                if (_timeSinceLastReload > 2.5f)
                {
                    _timeSinceLastReload = 0;
                    isAttacking = true;
                }
                break;
        }
    }
    private void OnShoot()
    {
        RangedAttackData rangedAttackData = _attackSO as RangedAttackData;
        float projectilesAngleSpace = rangedAttackData.multipleProjectilesAngel;
        int numberOfProjectilesPerShot = rangedAttackData.numberofProjectilesPershot;

        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * rangedAttackData.multipleProjectilesAngel;
        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-rangedAttackData.spread, rangedAttackData.spread);
            angle += randomSpread;
            _animController.SetAnimationActive(true);
            CreateProjectile(rangedAttackData, angle);
        }
    }
    private void CreateProjectile(RangedAttackData rangedAttackData, float angle)
    {
        _projectileManager.ShootBullet(projectileSpawnPosition.position, RotateVector3(_aimDirection, angle), rangedAttackData);
    }
    private Vector2 RotateVector3(Vector3 v, float degree)
    {
        return Quaternion.Euler(0, degree, degree) * v;
    }
}