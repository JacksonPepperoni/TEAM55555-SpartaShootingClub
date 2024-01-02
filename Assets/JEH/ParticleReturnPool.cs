using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleReturnPool : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        if (_particleSystem != null)
            Destroy(this.gameObject, _particleSystem.duration);
        else
            Destroy(this.gameObject, 1f);
    }

}
