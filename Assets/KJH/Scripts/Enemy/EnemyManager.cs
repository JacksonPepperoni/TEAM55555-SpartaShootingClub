using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    static public EnemyManager instance;

    [SerializeField] private GameObject enemyPrefabs;

    [SerializeField] private int currentSpawnCount;
    private int waveSpaawnCount;

    [SerializeField] private Transform spawnPositionsRoot;
    private List<Transform> spawnPositions = new List<Transform>();
    private List<GameObject> enemyList = new List<GameObject>();

    private void Awake()
    {
        instance = this;

        for (int i = 0; i < spawnPositionsRoot.childCount; i++)
        {
            spawnPositions.Add(spawnPositionsRoot.GetChild(i));
        }
    }

    private void Start()
    {
        Init();
        firstSetting();
    }

    private void Init()
    {
        //enemyPrefabs = new List<GameObject>();
        currentSpawnCount = 0;
        waveSpaawnCount = 4;
    }

    private void firstSetting()
    {
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            //int prefabIdx = Random.Range(0, enemyPrefabs.Count);
            GameObject enemy = Instantiate(enemyPrefabs, spawnPositions[i].position, Quaternion.Euler(90, 0, 0));
            enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;
            enemy.GetComponent<BoxCollider>().enabled = false;
            enemyList.Add(enemy);
        }
        for(int i=0; i<waveSpaawnCount;)
        {
            int posIdx = Random.Range(0, enemyList.Count);
            if (!enemyList[posIdx].GetComponent<Animator>().GetBool("IsActive"))
            {
                enemyList[posIdx].GetComponent<EnemyController>().Respawn();
                enemyList[posIdx].GetComponent<BoxCollider>().enabled = true;
                i++;
                currentSpawnCount++;
            }
        }
     
    }
    private void OnEnemyDeath()
    {
        currentSpawnCount--;
        Invoke("SpawnEnemy",3f);
    }
    private void SpawnEnemy()
    {
        while(true)
        {
            int spawnIdx = Random.Range(0, spawnPositions.Count);
            Debug.Log(spawnIdx);
            if(enemyList[spawnIdx].GetComponent<BoxCollider>().enabled==false)
            {
                Debug.Log("성공");
                enemyList[spawnIdx].GetComponent<EnemyController>().Respawn();
                break;
            }
            
        }
    }
}
