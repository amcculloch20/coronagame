using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private Damageable _playerDamageable;

    public TextMeshProUGUI deathText;
    public TextMeshProUGUI restartText;
    public GameObject blurImage;

    // Start is called before the first frame update
    void Start()
    {
        _playerDamageable = GameObject.FindGameObjectWithTag("Player").GetComponent<Damageable>();

        _playerDamageable.onHealthZero += LossScreen;

        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerMovement.GameIsOver)
        {
            Color tempColor = deathText.color;
            tempColor.a = Mathf.Lerp(tempColor.a, 1, 1.5f * Time.deltaTime);
            deathText.color = tempColor;

            if (tempColor.a >= 0.8)
            {
                Color tempColor2 = restartText.color;
                tempColor2.a = Mathf.Lerp(tempColor2.a, 1, 1.5f * Time.deltaTime);
                restartText.color = tempColor2;
            }

            if (Input.GetKeyDown("r"))
            {
                SceneManager.LoadScene("Main");
            }
        }


    }

    void LossScreen()
    {
        _playerMovement.GameIsOver = true;
        blurImage.transform.localScale = new Vector3(1, 1, 1);
    }
}
