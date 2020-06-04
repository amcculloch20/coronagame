using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;
    public float inputGravity = 10f;
    public Boolean GameIsOver;
    
    private Vector2 _input;
    private Vector2 _inputLerp;
    private Rigidbody2D _rb;

    private Camera _main;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _main = Camera.main;
        GameIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {

        UpdateInput();

        Vector3 screenPoint = Input.mousePosition;
        Vector3 position = transform.position;
        screenPoint.z = position.z;
        
        Vector3 mousePose = _main.ScreenToWorldPoint(screenPoint);
        float angle = Mathf.Atan2(
            mousePose.y - position.y,
            mousePose.x - position.x
        );
        
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.z = angle * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(rotation);
        
    }

    void UpdateInput()
    {
        float hor = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        if (!GameIsOver)
        {
            _input = new Vector2(hor * (float)Math.Sqrt(1 - Math.Pow(vert, 2) / 2), vert * (float)Math.Sqrt(1 - Math.Pow(hor, 2) / 2));
        }
        
    }

    private void FixedUpdate()
    {
        _inputLerp = Vector2.Lerp(_inputLerp, _input, Time.fixedDeltaTime * inputGravity);
        _rb.MovePosition(_rb.position + _inputLerp * (Time.fixedDeltaTime * speed));
    }
}
