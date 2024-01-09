using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTarget : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;

    private Rigidbody _rigid;
    private BoxCollider _boxCollider;
    [SerializeField] private GameObject prefabs;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _boxCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        maxHealth = 1;
        curHealth = maxHealth;
        Debug.Log("여긴 오자나");
    }
    

}
