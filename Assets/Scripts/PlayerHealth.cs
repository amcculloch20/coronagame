using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    
    public Image healthBar;
    public float smoothSpeed = 1f;
    public GameObject target;

    private Damageable _damageable;

    // Start is called before the first frame update
    void Start()
    {
        _damageable = GetComponent<Damageable>();
    }

    // Update is called once per frame
    void Update()
    {
        float fillAmount = _damageable.PercentHealth;

        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount,fillAmount, smoothSpeed * Time.deltaTime);
    }
}
