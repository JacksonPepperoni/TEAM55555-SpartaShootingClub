using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
    private ObjectPool objectPool;

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        objectPool = gameObject.AddComponent<ObjectPool>();
        GameObject bulletPrefab = ResourceManager.Instance.GetCache<GameObject>("Bullet");
        ObjectPool.Pool bulletPool = new()
        {
            prefab = bulletPrefab,
            size = 20,
            tag = "Bullet",
        };
        objectPool.AddPool(bulletPool);
        objectPool.SetInfo();

        return true;
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
