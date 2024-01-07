using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    static public EnemyManager instance;

    [SerializeField] private GameObject[] enemyPrefabs;


    [SerializeField] private int currentSpawnCount;
    private int waveSpaawnCount;

    [SerializeField] private Transform BaseEnemyspawnPositionsRoot;
    [SerializeField] private Transform AttackEnemyspawnPositionsRoot;
    private List<Transform> BaseEnemyspawnPositions = new List<Transform>();
    private List<Transform> AttackEnemyspawnPositions = new List<Transform>();
    private List<GameObject> enemyList = new List<GameObject>();


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < BaseEnemyspawnPositionsRoot.childCount; i++)
        {
            BaseEnemyspawnPositions.Add(BaseEnemyspawnPositionsRoot.GetChild(i));
        }

        for(int i=0; i<AttackEnemyspawnPositionsRoot.childCount;i++)
        {
            AttackEnemyspawnPositions.Add(AttackEnemyspawnPositionsRoot.GetChild(i));
        }
        currentSpawnCount = 0;
        waveSpaawnCount = 3;

        SetEnemy();
    }

    private void SetEnemy()
    {
        for (int i = 0; i < BaseEnemyspawnPositions.Count; i++)
        {
            //TODO : int prefabIdx = Random.Range(0, enemyPrefabs.Count);
            GameObject enemy = Instantiate(enemyPrefabs[0], BaseEnemyspawnPositions[i].position, Quaternion.Euler(-90, 0, 0));
            enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;
            enemyList.Add(enemy);
        }

        for(int i=0; i<AttackEnemyspawnPositions.Count; i++)
        {
            GameObject enemy = Instantiate(enemyPrefabs[1], AttackEnemyspawnPositions[i].position, Quaternion.Euler(-90, -90, 0));
            enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;
            enemyList.Add(enemy);
        }
        SpawnEnemy();
    }

    private void OnEnemyDeath()
    {
        Invoke("SpawnEnemy", 3f);
        currentSpawnCount--;
    }
    private void SpawnEnemy()
    {

        for (int i = currentSpawnCount; i < waveSpaawnCount;)
        {
            int spawnIdx = Random.Range(0, enemyList.Count);
            //애니메이션 컴포넌트 켜주고 => rigidbody도 켜주고
            if (!enemyList[spawnIdx].TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                enemyList[spawnIdx].GetComponent<EnemyController>().Respawn();
                currentSpawnCount++;
                i++;
            }
        }
    }
}