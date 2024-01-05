using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager instance;
    private ObjectPool objectPool;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        objectPool = GetComponent<ObjectPool>();
    }
    public void ShootBullet(Vector3 startPosition, Vector3 direction, RangedAttackData attackData)
    {
        GameObject obj = objectPool.SpawnFromPool(attackData.bulletNameTag);

        obj.transform.position = startPosition;
        EnemyRangedAttackController attackController = obj.GetComponent<EnemyRangedAttackController>();
        attackController.InitalizeAttack(direction, attackData, this);

        obj.SetActive(true);
    }
}
