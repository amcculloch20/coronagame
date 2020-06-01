using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupGrabber : MonoBehaviour
{
    public delegate void PickupAction(Pickup pickup);

    public Dictionary<Pickup.PickupItem, PickupAction> pickupActions = new Dictionary<Pickup.PickupItem, PickupAction>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Get pickup type
        Pickup pickup = other.gameObject.GetComponent<Pickup>();
        if (pickup)
        {
            Pickup.PickupItem type = pickup.type;
            pickupActions[type](pickup);
        }
    }
}
