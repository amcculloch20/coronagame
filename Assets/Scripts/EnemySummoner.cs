using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySummoner : MonoBehaviour
{
    public GameObject spawnObject;
    public Vector3 spawnVolume = Vector3.one;
    public float baseRate = 1f;
    public float growthRate = 1.1f;
    public bool spawnerEnabled = true;

    public float Rate => baseRate * Mathf.Pow(growthRate, Time.time - _startTime);

    private float _lastSpawn;
    private float _startTime;
    
    // Start is called before the first frame update
    void Start()
    {
        _startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnerEnabled && Time.time - _lastSpawn > (1 / Rate))
        {
            Spawn();
        }
    }
    
    // Draw spawn volume in editor
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawWireCube(transform.position, spawnVolume);
    }

    private void Spawn()
    {
        Vector3 location = transform.position - spawnVolume / 2;
        Vector3 locationAdjustment = new Vector3(
            Random.value * spawnVolume.x,
            Random.value * spawnVolume.y,
            Random.value * spawnVolume.z
        );

        location += locationAdjustment;

        Instantiate(
            spawnObject,
            location,
            Quaternion.identity
        );

        _lastSpawn = Time.time;
    }
}
