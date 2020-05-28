using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //[HideInInspector]
    public Transform player;
    public Transform enemy;
    public GameObject bulletprefab;
    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        shoot();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void bulletgo(Vector2 direction)
    { 
        enemy.Translate(direction * speed * Time.deltaTime);
        
    }
    void shoot()
    {
        GameObject b = Instantiate(bulletprefab) as GameObject;
        b.GetComponent<Bullet>().velocity = vectorToPlayer();
        b.transform.position = enemy.transform.position;
        Destroy(b,1f);
        
    }
    Vector2 vectorToPlayer()
    {
        return (player.position*2 - transform.position*2).normalized;
        
    }

}
