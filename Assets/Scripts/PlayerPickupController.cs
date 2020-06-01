using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickupController : MonoBehaviour
{
    [Header("Grabber")]
    public PickupGrabber grabber;

    [Header("Masks")] 
    public GameObject maskShield;
    
    private int _maskCount;
    private MaskShield _activeMask;
    
    // Start is called before the first frame update
    void Start()
    {
        grabber.pickupActions[Pickup.PickupItem.Mask] = MaskAction;
    }

    // Update is called once per frame
    void Update()
    {
        // If player is not wearing mask, spawn
        if (!_activeMask && _maskCount > 0)
        {
            GameObject maskGameObject = Instantiate(
                maskShield,
                transform.position,
                Quaternion.identity,
                transform
            );
            
            maskGameObject.transform.localRotation = Quaternion.Euler(0, 180, -90);
            
            _activeMask = maskGameObject.GetComponent<MaskShield>();

            _activeMask.player = gameObject;

            _maskCount -= 1;
        }
    }

    void MaskAction(Pickup mask)
    {
        _maskCount += 1;
        Destroy(mask.gameObject);
        Debug.Log($"Player has {_maskCount} masks");
    }
}
