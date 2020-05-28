using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth = 100;
    public Image healthBar;
    private float _currentHealth;
    public float smoothspeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float fillamt = _currentHealth / maxHealth;

        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount,fillamt, smoothspeed*Time.deltaTime);
    }

    public void Damage(float amount)
    {
        _currentHealth -= amount;
    }
}
