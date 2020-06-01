using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayBullet : MonoBehaviour
{
    public float spread = 45f;
    public int count = 5;
    public bool isParent;

    private bool _hasSpawnedChildren = false;

    private Bullet _bullet;

    // Start is called before the first frame update
    void Start()
    {
        _bullet = GetComponent<Bullet>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnChildren()
    {
        float angleInterval = spread / count;
        float angle = Mathf.Atan2(_bullet.velocity.y, _bullet.velocity.x);

        float minAngle = angle - spread / 2;
        float maxAngle = angle + spread / 2;

        for (float a = minAngle; a <= maxAngle; a += angleInterval)
        {
            
        }
    }
}
