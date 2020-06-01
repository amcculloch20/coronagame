using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskShield : MonoBehaviour
{
    public MaskShieldMesh maskShieldMesh;
    public ParticleSystem destroyParticle;

    [HideInInspector] 
    public GameObject player;

    private Damageable _damageable;
    private Animator _animator;
    
    private static readonly int DestroyTrigger = Animator.StringToHash("Destroy");
    private static readonly int DamageTrigger = Animator.StringToHash("Damage");

    // Start is called before the first frame update
    void Start()
    {
        maskShieldMesh.destroyDelegate = DestroyMask;
        
        _animator = maskShieldMesh.gameObject.GetComponent<Animator>();
        _damageable = GetComponent<Damageable>();

        _damageable.onDamage += OnDamage;
        _damageable.onHealthZero += OnHealthZero;
    }

    private void OnDamage(float _)
    {
        _animator.SetTrigger(DamageTrigger);
    }

    private void OnHealthZero()
    {
        // Particle
        ParticleSystem p = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(p.gameObject, p.main.duration);
        
        // Start destroying
        _animator.SetTrigger(DestroyTrigger);
            
        // Reset player mask
        PlayerPickupController pickupController = player.GetComponent<PlayerPickupController>();
    }

    private void DestroyMask()
    {
        Destroy(gameObject);
    }
}
