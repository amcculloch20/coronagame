using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 1f;

    private Vector2 _input;
    private Rigidbody2D _rb;

    private Camera _main;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

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

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _input * (Time.deltaTime * speed));
    }
}
