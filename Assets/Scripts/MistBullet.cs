using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MistBullet : MonoBehaviour
{
    public float damagePerSecond = 1f;
    public float intervalsPerSecond = 1f;
    public float speedDecayRate = 2f;
    public float lifetime = 5f;

    public float DamagePerInterval => damagePerSecond / intervalsPerSecond;
    private float _spawnTime;

    private List<Damageable> _damageablesInRange = new List<Damageable>();
    private List<float> _damageableCooldowns = new List<float>();
    
    private Bullet _bullet;

    // Start is called before the first frame update
    void Start()
    {
        _bullet = GetComponent<Bullet>();

        _spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        _bullet.speed = Mathf.Lerp(_bullet.speed, 0, speedDecayRate * Time.deltaTime);

        // Apply damage
        ApplyDamage();

        if (Time.time - _spawnTime >= lifetime)
        {
            _bullet.DetachTrail();
            Destroy(gameObject);
        }
    }

    void ApplyDamage()
    {
        for (int i = 0; i < _damageablesInRange.Count; i++)
        {
            // Update cooldown time
            _damageableCooldowns[i] -= Time.deltaTime;
            
            // If cooldown time is less than zero, cooldown is complete; apply damage and reset cooldown time
            if (_damageableCooldowns[i] <= 0)
            {
                _damageablesInRange[i].Damage(DamagePerInterval);
                _damageableCooldowns[i] = 1 / intervalsPerSecond;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Damageable damageable = other.gameObject.GetComponent<Damageable>();
        if (damageable)
        {
            _damageablesInRange.Add(damageable);
            _damageableCooldowns.Add(0f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Damageable damageable = other.gameObject.GetComponent<Damageable>();
        if (damageable)
        {
            _damageableCooldowns.Remove(_damageablesInRange.IndexOf(damageable));
            _damageablesInRange.Remove(damageable);
        }
    }
}
