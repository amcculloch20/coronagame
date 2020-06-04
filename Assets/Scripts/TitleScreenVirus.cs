using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenVirus : MonoBehaviour
{

    public float speed;
    public float waveAmplitude;
    public float lifetime = 10f;

    private float _spawnTime;
    
    // Start is called before the first frame update
    void Start()
    {
        _spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(
            Mathf.Sin(Time.time) * waveAmplitude,
            speed * Time.deltaTime,
            0,
            Space.World
        );

        if (Time.time - _spawnTime > lifetime)
        {
            Destroy(gameObject);
        }
    }
}
