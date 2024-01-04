using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            GameObject enemy = Instantiate(enemyPrefabs, spawnPositions[i].position, Quaternion.Euler(-90, 0, 0));
            enemy.GetComponent<HealthSystem>().OnDeath += OnEnemyDeath;
            //컴포넌트만 다 끌 수 있나
            enemyList.Add(enemy);
        }
        for (int i = 0; i < waveSpaawnCount;)
        {
            int posIdx = Random.Range(0, enemyList.Count);
            //애니메이션 컴포넌트 켜주고 => rigidbody도 켜주고
            if (!enemyList[posIdx].TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
            {
                enemyList[posIdx].GetComponent<EnemyController>().Respawn();
                currentSpawnCount++;
                i++;
            }
            //if (!enemyList[posIdx].GetComponent<Animator>().GetBool("IsActive"))
            //{
            //    enemyList[posIdx].GetComponent<EnemyController>().Respawn();
            //    //컴포넌트만 켜주기enemyList[posIdx].GetComponent<BoxCollider>().enabled = true;
            //    i++;
            //    currentSpawnCount++;
            //}
        }

    }
    private void OnEnemyDeath()
    {
        Invoke("SpawnEnemy",3f);
    }
    private void SpawnEnemy()
    {
        while(true)
        {
            int spawnIdx = Random.Range(0, spawnPositions.Count);
            Debug.Log(spawnIdx);

            if (enemyList[spawnIdx].GetComponent<Rigidbody>()==null)
            {
                Debug.Log("성공");
                gameObject.AddComponent<Rigidbody>().useGravity = false ;
                enemyList[spawnIdx].GetComponent<EnemyController>().Respawn();
                break;
            }
        }
    }
}
