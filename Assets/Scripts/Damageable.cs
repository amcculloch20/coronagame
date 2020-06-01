using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    public float maxHealth;
    public bool countAfterZero;

    public float PercentHealth => health / maxHealth;

    public delegate void OnHealthZeroDelegate();
    public delegate void OnDamageDelegate(float amount);

    public OnDamageDelegate onDamage;
    public OnHealthZeroDelegate onHealthZero;

    [HideInInspector] public float health;
    [HideInInspector] public bool dead;
    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        onDamage += OnDamageDebug;
        onHealthZero += OnHealthZeroDebug;
    }

    public void Damage(float amount)
    {
        if (!countAfterZero && dead) return;
        
        health -= amount;
        onDamage(amount);

        if (health <= 0)
        {
            dead = true;
            onHealthZero();
        }
    }

    private void OnDamageDebug(float amount)
    {
        Debug.Log($"Damage to {gameObject.name}: {amount}");
    }

    private void OnHealthZeroDebug()
    {
        Debug.Log($"Health zero for {gameObject.name}");
    }
}
