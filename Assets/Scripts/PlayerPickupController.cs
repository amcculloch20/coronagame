using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickupController : MonoBehaviour
{
    [Header("Grabber")]
    public PickupGrabber grabber;

    [Header("Masks")] 
    public GameObject maskShield;

    [Header("UI")]
    public TMP_Text count;
    public Animator menuMaskAnimator;
    
    private int _maskCount;
    private MaskShield _activeMask;

    private PlayerShooter _shooter;
    private static readonly int CountUp = Animator.StringToHash("CountUp");
    private static readonly int CountDown = Animator.StringToHash("CountDown");

    // Start is called before the first frame update
    void Start()
    {
        _shooter = GetComponent<PlayerShooter>();
        
        grabber.pickupActions[Pickup.PickupItem.Mask] = MaskAction;
        grabber.pickupActions[Pickup.PickupItem.Sanitizer] = SanitizerAction;
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
            
            count.text = _maskCount.ToString();
            menuMaskAnimator.SetTrigger(CountDown);
        }
    }

    void MaskAction(Pickup mask)
    {
        _maskCount += 1;
        
        count.text = _maskCount.ToString();
        menuMaskAnimator.SetTrigger(CountUp);
        
        Destroy(mask.gameObject);
    }

    void SanitizerAction(Pickup sanitizer)
    {
        SanitizerPickup data = sanitizer.GetComponent<SanitizerPickup>();
        _shooter.UpdateFill(data.sanitizerGun, data.refillAmount);
        
        Destroy(sanitizer.gameObject);
    }
}
