using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickupItem
    {
        Mask,
        Sanitizer
    }

    public PickupItem type;
}
