using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{

    public VirusMeshController meshController;

    [Header("Appearance")] 
    public ParticleSystem deathParticle;

    // so we can access this with other components
    [HideInInspector]
    public Transform player;
    
    private Damageable _damageable;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _damageable = GetComponent<Damageable>();

        _damageable.onDamage += meshController.AnimateDamage;
        _damageable.onHealthZero += OnDeath;
        
        // pair mesh controller
        meshController.enemy = this;
    }

    void OnDeath()
    {
        ParticleSystem p = Instantiate(deathParticle, transform.transform.position, Quaternion.identity);
        Destroy(p.gameObject, p.main.duration);
        
        Destroy(gameObject);
    }

    // MISC UTIL

    public Vector2 VectorToPlayer()
    {
        return (player.position - transform.position);
    }
    
}
