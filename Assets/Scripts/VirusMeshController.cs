using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;

public class VirusMeshController : MonoBehaviour
{
    [HideInInspector] public Enemy enemy;

    private Animator _animator;
    
    private static readonly int Shoot = Animator.StringToHash("Shoot");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimateShoot()
    {
        _animator.SetTrigger(Shoot);
    }

    public void SpawnBullet()
    {
        enemy.SpawnBullet();
    }
}
