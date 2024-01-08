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
    //private List<Transform> AttackEnemyspawnPositions = new List<Transform>();
    private List<GameObject> enemyList = new List<GameObject>();

    private int level = 1;


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

        //for(int i=0; i<AttackEnemyspawnPositionsRoot.childCount;i++)
        //{
        //    AttackEnemyspawnPositions.Add(AttackEnemyspawnPositionsRoot.GetChild(i));
        //}
        currentSpawnCount = 0;
        waveSpaawnCount = 3;

        SetEnemy();
    }

    private void SetEnemy()
    {
        for (int i = 0; i < BaseEnemyspawnPositions.Count; i++)
        {
            //TODO : int prefabIdx = Random.Range(0, enemyPrefabs.Count);
            if (i % 2 == 0)
            {
                GameObject enemy = Instantiate(enemyPrefabs[0], BaseEnemyspawnPositions[i].position, Quaternion.Euler(-90, 90 , 0));
                enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;
                enemyList.Add(enemy);
            }
            else
            {
                GameObject enemy = Instantiate(enemyPrefabs[0], BaseEnemyspawnPositions[i].position, Quaternion.Euler(-90, -90, 0));
                enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;
                enemyList.Add(enemy);
            }

        }

        //for(int i=0; i<AttackEnemyspawnPositions.Count; i++)
        //{
        //    GameObject enemy = Instantiate(enemyPrefabs[1], AttackEnemyspawnPositions[i].position, Quaternion.Euler(-90, -90, 0));
        //    enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;
        //    enemyList.Add(enemy);
        //}
        SpawnEnemy();
    }

    private void OnEnemyDeath()
    {
        switch(level)
        {
            case 1:
                currentSpawnCount--;
                break;
            case 2:
                Invoke("Sp awnEnemy", 3f);
                currentSpawnCount--; 
                break;
            case 3:
                break; ;
        }

    }
    private void SpawnEnemy()
    {
        switch(level)
        {
            case 1:
                InvokeRepeating("OneLIneEnemySpawn", 0f, 3f);
                break;
            case 2:
                //for (int i = currentSpawnCount; i < waveSpaawnCount;)
                //{
                //    int spawnIdx = Random.Range(0, enemyList.Count);
                //    애니메이션 컴포넌트 켜주고 => rigidbody도 켜주고
                //    if (!enemyList[spawnIdx].TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                //    {
                //        enemyList[spawnIdx].GetComponent<EnemyController>().Respawn();
                //        currentSpawnCount++;
                //        i++;
                //    }
                //}
                break;
            case 3:
                break;
        }

    }
    private void OneLIneEnemySpawn()
    {
        if(currentSpawnCount == BaseEnemyspawnPositions.Count / 2)
        {
            CancelInvoke("OneLIneEnemySpawn");
            return;
        }
        int randomPosition = currentSpawnCount*2 + Random.Range(0, 2);
        Debug.Log(randomPosition);
        enemyList[randomPosition].GetComponent<EnemyController>().Respawn();
        currentSpawnCount++;
    }
}