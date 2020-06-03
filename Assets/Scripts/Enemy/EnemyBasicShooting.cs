using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyBasicShooting : MonoBehaviour
{

    public GameObject bulletPrefab;

    // if we want it only to shoot depending on distance
    public EnemyTargetDistanceMovement enemyTargetDistanceMovement;
    
    [Header("This only applies if there is no movement script attached.")]
    public float shootingRadius = 3f;
    
    [Space]
    public float cooldown = 2f;

    // to auto-assign
    private Enemy _enemy;
    private Transform _player;
    private bool _usingMovementScript;
    
    // for Maths and such
    private float _lastShoot;
    
    // Start is called before the first frame update
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _player = _enemy.player;
        _usingMovementScript = enemyTargetDistanceMovement != null;
    }

    // Update is called once per frame
    void Update()
    {
        if (AtShootDistance() && Time.time - _lastShoot > cooldown)
        {
            Shoot();
        }
    }
    
    void Shoot()
    {
        _lastShoot = Time.time;
        
        // TODO: modular mesh controller stuff
        // _enemy.meshController.AnimateShoot();
        SpawnBullet();
    }
    
    public void SpawnBullet()
    {
        GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        b.GetComponent<Bullet>().velocity = _enemy.VectorToPlayer().normalized;
    }

    bool AtShootDistance()
    {
        return (_usingMovementScript ? enemyTargetDistanceMovement.AtTargetDistance() : _enemy.VectorToPlayer().magnitude < shootingRadius);
    }
}
