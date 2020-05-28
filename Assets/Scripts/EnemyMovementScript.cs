using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovementScript : MonoBehaviour
{
    public float speed = 1f;


    private Enemy enemyscript;

    

    // Start is called before the first frame update
    void Start()
    {
        enemyscript = GetComponent<Enemy>();
        
    }

    // Update is called once per frame
    void Update()
    {
     
        transform.position = Vector2.MoveTowards(transform.position, enemyscript.player.position, speed * Time.deltaTime);

       
    }
}
