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
        if (isParent && !_hasSpawnedChildren)
        {
            SpawnChildren();
        }
    }

    void SpawnChildren()
    {
        _hasSpawnedChildren = true;
        
        float angleInterval = spread / count;
        float angle = Mathf.Atan2(_bullet.velocity.y, _bullet.velocity.x) * Mathf.Rad2Deg;

        float minAngle = angle - spread / 2;
        float maxAngle = angle + spread / 2;

        for (float a = minAngle; a <= maxAngle; a += angleInterval)
        {
            // Calculate velocity
            Vector2 velocity = new Vector2(
                Mathf.Cos(a * Mathf.Deg2Rad),
                Mathf.Sin(a * Mathf.Deg2Rad)
            );
            
            // Instantiate child
            GameObject child = Instantiate(gameObject, transform.position, Quaternion.identity);
            Bullet childBullet = child.GetComponent<Bullet>();
            childBullet.velocity = velocity;
            SprayBullet childSprayBullet = child.GetComponent<SprayBullet>();
            childSprayBullet.isParent = false;
        }
        
        // Destroy self
        Destroy(gameObject);
    }
}
