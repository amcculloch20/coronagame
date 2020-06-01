using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooter : MonoBehaviour
{
    [System.Serializable]
    public class SanitizerInventoryEntry
    {
        public SanitizerGun sanitizerGun;
        
        public float fill;
        public float cost;
    }

    [Header("Sanitizer Inventory")] 
    [SerializeField]
    public SanitizerInventoryEntry[] sanitizerInventory;

    public int currentGunIndex = 0;

    public SanitizerGun CurrentGun
    {
        get => sanitizerInventory[currentGunIndex].sanitizerGun;
        set
        {
            int index = sanitizerInventory
                .Select((e, i) => new {entry = e, index = i})
                .First(e => e.entry.sanitizerGun == value)
                .index;

            currentGunIndex = index;
        }
    }

    [Header("UI")] 
    public GunMenuController menuController;

    private float _lastShoot;
    
    private Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;

        menuController.GunColor = CurrentGun.color;
    }

    // Update is called once per frame
    void Update()
    {
        // Shoot if click
        if (
            Input.GetAxis("Fire1") > 0.5f
            && Time.time - _lastShoot > CurrentGun.cooldown 
            && sanitizerInventory[currentGunIndex].fill > sanitizerInventory[currentGunIndex].cost
        )
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 position = transform.position;
        GameObject bullet = Instantiate(CurrentGun.bullet, position, Quaternion.identity);
        
        bullet.GetComponent<Bullet>().velocity = ShootVector();

        sanitizerInventory[currentGunIndex].fill -= sanitizerInventory[currentGunIndex].cost;
        menuController.fillAmount = sanitizerInventory[currentGunIndex].fill;

        _lastShoot = Time.time;
    }

    Vector2 ShootVector()
    {
        Vector3 position = transform.position;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = position.z;
        
        Vector3 screenPos = _mainCamera.ScreenToWorldPoint(mousePos);
        screenPos.z = position.z;
        
        return (screenPos - position).normalized;
    }
}
