using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 angularVelocity;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(angularVelocity * (rotationSpeed * Time.deltaTime));
    }
}
