using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class TargetController : MonoBehaviour
{
    [SerializeField] List<Collider> colliders;
    private Transform _tranform;
    private float _activeRotZ = 0;
    private float _inactiveRotZ = 90;
    private float _spawnTime = 2;

    private void Awake()
    {
        _tranform = GetComponent<Transform>();
        SetColliders(false);
        Invoke("ReSpawn", 2f);
    }

    private void SetColliders(bool isActive)
    {
        for(int i=0; i<colliders.Count ;i++)
        {
            colliders[i].enabled = isActive;
        }
    }

    public void ReSpawn()
    {
        StartCoroutine(ActiveTarget());
    }

    public void Die()
    {
        StartCoroutine(DeActiveTarget());
    }

    private IEnumerator ActiveTarget()
    {
        _tranform.DORotate(new Vector3(0,0, _activeRotZ), _spawnTime);
        yield return new WaitForSeconds(_spawnTime);
        _tranform.DOKill();
        _tranform.rotation = quaternion.Euler(0,0, _activeRotZ);
        GetComponent<HealthSystem>().InitHealthSystem();
        SetColliders(true);
    }

    private IEnumerator DeActiveTarget()
    {
        SetColliders(false);
        _tranform.DORotate(new Vector3(0,0, _inactiveRotZ), _spawnTime);
        yield return new WaitForSeconds(_spawnTime);
        _tranform.DOKill();
        _tranform.rotation = quaternion.Euler(0,0, _inactiveRotZ);

    }
}
