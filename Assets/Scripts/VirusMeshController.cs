using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class VirusMeshController : MonoBehaviour
{
    
    // enemy will set this 
    [HideInInspector] public Enemy enemy;

    private Animator _animator;
    
    // private static readonly int ShootTrigger = Animator.StringToHash("Shoot");
    private static readonly int DamageTrigger = Animator.StringToHash("Damage");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // public void AnimateShoot()
    // {
    //     _animator.SetTrigger(ShootTrigger);
    // }

    public void AnimateDamage(float _)
    {
        Debug.Log("ahw");
        _animator.SetTrigger(DamageTrigger);
    }

    // public void SpawnBullet()
    // {
    //     enemy.SpawnBullet();
    // }
}
