using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashBullet : MonoBehaviour
{

    public GameObject fragment;

    public int fragmentCount = 20;
    public float fuseTime = 3f;

    private float _spawnTime;

    private Bullet _bullet;
    
    // Start is called before the first frame update
    void Start()
    {
        _bullet = GetComponent<Bullet>();
        _bullet.onHitDelegate += OnHit;
        
        _spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        // If fuse time runs out before hit, just perform action
        if (Time.time - _spawnTime > fuseTime)
        {
            _bullet.ManageParticlesOnHit();
            OnHit();
            
            Destroy(gameObject);
        }
    }

    public void OnHit(Collision2D _ = null)
    {
        float angleInterval = Mathf.PI * 2 / fragmentCount;

        for (float a = 0; a < Mathf.PI * 2; a += angleInterval)
        {
            // Calculate velocity
            Vector2 velocity = new Vector2(Mathf.Cos(a), Mathf.Sin(a));
            
            // Spawn children
            GameObject bullet = Instantiate(fragment, transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().velocity = velocity;
        }
    }
}
