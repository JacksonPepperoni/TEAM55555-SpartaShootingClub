using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    //private ProjectileManager _projectileManager;

    [SerializeField] private Transform projectileSpawnPosition;
    //private Vector2 _aimDirection = Vector2.right;

    private EnemyStatsHandler _stats { get; set; }
    private AttackSO _attackSO;

    private float _timeSinceLastAttack = float.MaxValue;
    private bool isAttacking;

    private void Start()
    {
        //_projectileManager = ProjectileManager.instance;
        isAttacking = false;
    }

    private void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        if (_stats.currentStats.attackSO==null)
            return;
        if (_timeSinceLastAttack <= _stats.currentStats.attackSO.delay)
        {
            _timeSinceLastAttack += Time.deltaTime;
        }
        if (isAttacking && _timeSinceLastAttack > _stats.currentStats.attackSO.delay)
        {
            _timeSinceLastAttack = 0;
            OnShoot();
        }
    }
    //private void OnAim(Vector2 newAimdirection)
    //{
    //    _aimDirection = newAimdirection;
    //}
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
            CreateProjectile(rangedAttackData, angle);
        }
    }
    private void CreateProjectile(RangedAttackData rangedAttackData, float angle)
    {
        //_projectileManager.ShootBullet(projectileSpawnPosition.position, RotateVector2(_aimDirection, angle), rangedAttackData);
    }
    private Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}