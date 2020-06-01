using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public Transform player;
    public float mouseInfluence = .5f;

    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }
    
    void LateUpdate()
    {
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 posLerp = Vector2.Lerp(player.position, new Vector2(mousePos.x, mousePos.y), mouseInfluence * .5f);
        Vector3 target = new Vector3(posLerp.x, posLerp.y, this.transform.position.z);
        
        // TODO: fix jitter on smooth movement
        // transform.position = Vector3.SmoothDamp(transform.position, target, ref _velocity, smoothTime);
        transform.position = target;
    }
}
