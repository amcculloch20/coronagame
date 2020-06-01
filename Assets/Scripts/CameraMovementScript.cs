using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public Transform player;
    public float smoothTime;

    private Vector3 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 target = new Vector3(player.position.x, player.position.y, this.transform.position.z);
        
        // TODO: fix jitter on smooth movement
        // transform.position = Vector3.SmoothDamp(transform.position, target, ref _velocity, smoothTime);
        transform.position = target;
    }
}
