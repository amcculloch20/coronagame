using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public Vector3 spawnVolume = Vector3.one;
    public float rate = 3f;
    public bool spawnerEnabled = true;

    private float _lastSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnerEnabled && Time.time - _lastSpawn > (1 / rate))
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
