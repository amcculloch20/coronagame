using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class GunMenuController : MonoBehaviour
{
    [System.Serializable]
    public class GunIcon
    {
        public SanitizerGun gun;
        public Image fillImage;
        public Animator meshAnimator;

        public float fillAmount;
        public bool active;
    }
    
    [Header("AmmoBar")]
    public Image fill;
    public Image background;

    public float fillAmount;
    public float smoothSpeed;

    [Header("GunIcons")] [SerializeField]
    public List<GunIcon> gunIcons = new List<GunIcon>();

    private Color _color;
    private static readonly int Active = Animator.StringToHash("Active");

    public Color GunColor
    {
        get => _color;
        set => UpdateColor(value);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set to default
        _color = fill.color;
    }

    // Update is called once per frame
    void Update()
    {
        fill.fillAmount = Mathf.Lerp(fill.fillAmount,fillAmount, smoothSpeed * Time.deltaTime);
        
        UpdateGunIcons();
    }

    private void UpdateColor(Color color)
    {
        fill.color = color;
        Color.RGBToHSV(color, out float h, out float s, out float v);
        v /= 2;
        
        Color darker = Color.HSVToRGB(h, s, v);
        darker.a = 0.8f;

        background.color = darker;
    }

    private void UpdateGunIcons()
    {
        foreach (GunIcon gunIcon in gunIcons)
        {
            gunIcon.fillImage.fillAmount = Mathf.Lerp(
                gunIcon.fillImage.fillAmount, 
                gunIcon.fillAmount, 
                smoothSpeed * Time.deltaTime
            );
        }
    }

    public void SetActiveGun(SanitizerGun gun)
    {
        // Set correct gun active
        GunIcon icon = gunIcons.First(g => g.gun == gun);

        icon.active = true; 
        icon.meshAnimator.SetBool(Active, true);
        
        // Set previous active gun inactive
        GunIcon inactiveIcon = gunIcons.FirstOrDefault(g => g.gun != gun && g.active);

        if (inactiveIcon != null)
        {
            inactiveIcon.active = false;
            inactiveIcon.meshAnimator.SetBool(Active, false);
        }
    }

    public void SetGunFill(SanitizerGun gun, float gunFill)
    {
        GunIcon icon = gunIcons.First(g => g.gun == gun);
        icon.fillAmount = gunFill;
    }
}
