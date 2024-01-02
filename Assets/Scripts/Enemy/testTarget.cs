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
    }
    private void Update()
    {
        if (curHealth <= 0)
        {
            OnDie();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Bullet")
        {
            Debug.Log(curHealth);
            curHealth--;
            
        }    
    }
    private void OnDie()
    {
        Invoke("Respawn", 2f);
        gameObject.SetActive(false);
    }
    private void Respawn()
    {
        Debug.Log("¸®½ºÆù");
        Destroy(gameObject);
        Instantiate(prefabs,transform.position, Quaternion.identity);
    }
}
