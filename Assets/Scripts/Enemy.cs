using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 10f;
    public float targetRadius = 3f;
    public float maxRadius = 2.5f;

    public VirusMeshController meshController;

    [Header("Shooting")] 
    public GameObject bulletPrefab;

    public float shootingRadius = 3f;
    public float cooldown = 2f;

    private float _lastShoot;
    
    private Transform _player;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rb = GetComponent<Rigidbody2D>();

        _lastShoot = 0;
        
        meshController.enemy = this;
    }

    // Update is called once per frame
    void Update()
    {
        if ((_player.position - transform.position).magnitude < shootingRadius && Time.time - _lastShoot > cooldown)
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float distance = (_player.position - transform.position).magnitude;
        
        // Clamp at -0.5f so enemies back away from player
        float speedMultiplier = Mathf.Clamp((distance - maxRadius) / targetRadius, -0.5f, 1);
        
        _rb.MovePosition(_rb.position + VectorToPlayer() * (speedMultiplier * speed * Time.deltaTime));
    }

    void Shoot()
    {
        _lastShoot = Time.time;
        meshController.AnimateShoot();
    }

    public void SpawnBullet()
    {
        GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        b.GetComponent<Bullet>().velocity = VectorToPlayer();
    }
    
    Vector2 VectorToPlayer()
    {
        return (_player.position - transform.position).normalized;
    }

}
