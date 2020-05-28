using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySummoner : MonoBehaviour
{
    public GameObject enemy;
    public float enemycount;


    private Camera _mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;

        for (int i = 0; i < enemycount; i++)
        {

            Spawn();

        }

    }

    // Update is called once per frame
    void Update()

    {
       


    }

    
    void Spawn()
    {
        Vector3 position = new Vector3 (Random.value*10-5,Random.value*10-5,0);


        GameObject spawned = Instantiate(
            enemy,
            position,
            Quaternion.identity
            );
        //Destroy(spawned, 10f);
    }
   
}
