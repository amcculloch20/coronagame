using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSync : MonoBehaviour
{

    public bool sync = true;
    public float frameRate = 60;
    
    void Start()
    {
        Time.fixedDeltaTime = 1.0f / (sync ? Screen.currentResolution.refreshRate : frameRate);
    }

}
