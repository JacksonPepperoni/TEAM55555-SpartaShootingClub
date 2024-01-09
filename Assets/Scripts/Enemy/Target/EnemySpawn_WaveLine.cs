using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemySpawn_WaveLine : MonoBehaviour
{
    [SerializeField] private Transform targetSpawnRoot;

    private GameObject _targetPrefab;
    private List<Transform> _targetSpawnPositions;
    private List<TargetController> _targets;

    private int _currentSpawnCount;
    private int _waveSpawnCount;

    private void Start()
    {
        Invoke("Init", 0.1f);
    }

    public void Init()
    {
        _targetPrefab = ResourceManager.Instance.GetCache<GameObject>("Target");
        InitLocations();
        InitTargets();
    }

    private void InitLocations()
    {
        _waveSpawnCount = targetSpawnRoot.childCount;
        _currentSpawnCount = _waveSpawnCount;

        _targetSpawnPositions = new List<Transform>();
        for (int i = 0; i < _waveSpawnCount; i++)
        {
            _targetSpawnPositions.Add(targetSpawnRoot.GetChild(i));
        }
    }

    private void InitTargets()
    {
        _targets = new List<TargetController>();
        Transform root = EnemyManager.Instance.Root;
        for(int i=0; i < _waveSpawnCount ; i++)
        {
            GameObject target = Instantiate(_targetPrefab, _targetSpawnPositions[i].position, Quaternion.Euler(0, 0, -90));
            target.transform.parent = root;
            _targets.Add(target.GetComponent<TargetController>());
            target.GetComponent<HealthSystem_Target>().OnDeath += OnEnemyDeath;
        }
    }

    private void RespawnAllTargets()
    {
        _currentSpawnCount = _waveSpawnCount;
        for(int i=0 ; i < _targets.Count; i++)
        {
            _targets[i].ReSpawn();
        }
    }

    public void OnEnemyDeath()
    {
        _currentSpawnCount--;

        if(_currentSpawnCount == 0)
        {
            Invoke("RespawnAllTargets", 3f);
        }
    }
    
}
