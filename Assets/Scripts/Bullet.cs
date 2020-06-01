using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector]
    public Vector2 velocity;
    
    public float speed;
    public float power;

    public ParticleSystem trailParticle;
    public ParticleSystem hitParticle;

    public delegate void OnHitDelegate(Collision2D collision);
    public OnHitDelegate onHitDelegate;

    private Collider2D _collider;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();

        onHitDelegate += OnHit;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed*Time.deltaTime*velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onHitDelegate(collision);
        
        // Destroy game object after all delegate methods called
        Destroy(gameObject);
    }

    private void OnHit(Collision2D collision)
    {
        // Cause damage
        Damageable damageable = collision.collider.gameObject.GetComponent<Damageable>();
        if (damageable)
        {
            damageable.Damage(power);
        }
        
        // Detach trail particle to avoid particles disappearing
        trailParticle.Stop();
        trailParticle.gameObject.transform.parent = null;
        Destroy(trailParticle.gameObject, trailParticle.main.duration);
            
        // Spawn hit particle
        GameObject p = Instantiate(hitParticle, transform.position, Quaternion.identity).gameObject;
        Destroy(p, hitParticle.main.duration);
    }

}
