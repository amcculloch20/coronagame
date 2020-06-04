using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashBulletFrag : MonoBehaviour
{
    public float decayTime = 1.5f;
    public float minDamage = 5f;

    private Vector3 _baseScale;
    private float _baseDamage;
    private float _spawnTime;

    private Bullet _bullet;
    
    // Start is called before the first frame update
    void Start()
    {
        _bullet = GetComponent<Bullet>();

        _baseScale = transform.localScale;
        _baseDamage = _bullet.power;

        _spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // Percent through life
        float life = (Time.time - _spawnTime) / decayTime;

        transform.localScale = Vector3.Lerp(_baseScale, Vector3.zero, life);
        _bullet.power = Mathf.Lerp(_baseDamage, minDamage, life);
        
        // If life is over, detach trail and destroy
        if (life >= 1)
        {
            _bullet.DetachTrail();
            Destroy(gameObject);
        }
    }
}
