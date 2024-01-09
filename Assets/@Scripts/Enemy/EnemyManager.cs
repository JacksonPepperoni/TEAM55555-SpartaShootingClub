using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private GameObject[] enemyPrefabs;

    [SerializeField] private Transform LineMapEnemyspawnPositionsRoot;
    [SerializeField] private Transform GroundMapBaseEnemyspawnPositionsRoot;
    [SerializeField] private Transform GroundMapAttackEnemyspawnPositionsRoot;

    private List<Transform> EnemyspawnPositions;
    private List<Transform> AttackEnemyspawnPositions;

    private int currentSpawnCount;
    private int waveSpawnCount;

    private List<GameObject> enemyList;

    public int level = 0;

    private Transform root;

    public Transform Root
    {
        get
        {
            if (root == null)
                root = new GameObject("Enemy Root").transform;
            return root;
        }
    }


    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        enemyPrefabs = new GameObject[3];
        enemyPrefabs[0] = ResourceManager.Instance.GetCache<GameObject>("BaseTarget");
        enemyPrefabs[1] = ResourceManager.Instance.GetCache<GameObject>("AttackTarget");
        enemyPrefabs[2] = ResourceManager.Instance.GetCache<GameObject>("Target");

        LineMapEnemyspawnPositionsRoot = ResourceManager.Instance.GetCache<GameObject>("LineMapEnemySpawnPositionsRoot").transform;
        GroundMapBaseEnemyspawnPositionsRoot = ResourceManager.Instance.GetCache<GameObject>("GroundMapEnemySpawnPositions").transform.GetChild(0);
        GroundMapAttackEnemyspawnPositionsRoot = ResourceManager.Instance.GetCache<GameObject>("GroundMapEnemySpawnPositions").transform.GetChild(1);


        return true;
    }

    public void SetEnemy()
    {
        DespawnEnemy();

        SetSpawnPosition();

        switch (level)
        {
            case 0:
                break;
            case 1:
                for (int i = 0; i < EnemyspawnPositions.Count; i++)
                {
                    GameObject enemy = Instantiate(enemyPrefabs[0], EnemyspawnPositions[i].position, Quaternion.Euler(-90, 90 + (-180 * (i % 2)), 0));
                    enemy.transform.parent = root;
                    enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;
                    enemyList.Add(enemy);
                }
                break;
            case 2:
                break;
            case 3:
                for (int i = 0; i < EnemyspawnPositions.Count; i++)
                {
                    GameObject enemy = Instantiate(enemyPrefabs[0], EnemyspawnPositions[i].position, Quaternion.Euler(-90, 0 , 0));
                    enemy.transform.parent = root;
                    enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;
                    enemyList.Add(enemy);
                }
                for(int i=0; i<AttackEnemyspawnPositions.Count; i++)
                {
                    GameObject enemy = Instantiate(enemyPrefabs[1], AttackEnemyspawnPositions[i].position, Quaternion.Euler(-90, -90, 0));
                    enemy.transform.parent = root;
                    enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;
                    enemyList.Add(enemy);
                }
                break;
        }

        SpawnEnemy();
    }

    private void DespawnEnemy()
    {
        if (enemyList != null)
        {
            while (enemyList.Count > 0)
            {
                Destroy(enemyList[0]);
                enemyList.RemoveAt(0);
            }
        }

        enemyList = new List<GameObject>();
    }

    private void SetSpawnPosition()
    {
        EnemyspawnPositions = new List<Transform>();
        AttackEnemyspawnPositions = new List<Transform>();
        CancelInvoke("OneLIneMapEnemySpawn");
        currentSpawnCount = 0;
        switch (level)
        {
            case 0:
                break;
            case 1:
                for (int i = 0; i < LineMapEnemyspawnPositionsRoot.childCount; i++)
                {
                    EnemyspawnPositions.Add(LineMapEnemyspawnPositionsRoot.GetChild(i));
                }
                waveSpawnCount = 3;
                break;
            case 2:
                break;
            case 3:
                for (int i = 0; i < GroundMapBaseEnemyspawnPositionsRoot.childCount; i++)
                {
                    EnemyspawnPositions.Add(GroundMapBaseEnemyspawnPositionsRoot.GetChild(i));
                }

                for (int i = 0; i < GroundMapAttackEnemyspawnPositionsRoot.childCount; i++)
                {
                    AttackEnemyspawnPositions.Add(GroundMapAttackEnemyspawnPositionsRoot.GetChild(i));
                }
                waveSpawnCount = EnemyspawnPositions.Count;
                break;
        }
    }

    private void OnEnemyDeath()
    {
        switch(level)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                Invoke("SpawnEnemy", 3f);
                currentSpawnCount--;
                break; ;
        }

    }
    private void SpawnEnemy()
    {
        switch(level)
        {
            case 0:
                break;
            case 1:
                InvokeRepeating("OneLIneMapEnemySpawn", 0f, 3f);
                break;
            case 2:
                break;
            case 3:
                GroundMapEnemySpawn();
                break;
        }

    }
    private void GroundMapEnemySpawn()
    {
        for (int i = currentSpawnCount; i < waveSpawnCount;)
        {
            int spawnIdx = Random.Range(0, enemyList.Count);

            if (!enemyList[spawnIdx].TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                enemyList[spawnIdx].GetComponent<EnemyController>().Respawn();
                currentSpawnCount++;
                i++;
            }
        }
    }
    private void OneLIneMapEnemySpawn()
    {
        if(currentSpawnCount == EnemyspawnPositions.Count / 2)
        {
            CancelInvoke("OneLIneEnemySpawn");
            return;
        }
        int randomPosition = currentSpawnCount*2 + Random.Range(0, 2);
        enemyList[randomPosition].GetComponent<EnemyController>().Respawn();
        currentSpawnCount++;
    }
}