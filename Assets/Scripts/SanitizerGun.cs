using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SanitizerGun", menuName = "ScriptableObjects/SanitizerGun", order = 1)]
public class SanitizerGun : ScriptableObject
{
    public string gunName;
    public Color color;
    public float rate;

    public float cooldown => 1 / rate;
    
    public GameObject bullet;
}
