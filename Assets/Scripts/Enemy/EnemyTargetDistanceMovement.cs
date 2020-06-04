using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyTargetDistanceMovement : MonoBehaviour
{
    
    public float speed = 10f;
    public float targetRadius = 3f;
    public float targetRadiusVariance = 2f;

    // private shit (to auto-assign)
    private Enemy _enemy;
    private Transform _player;
    private Rigidbody2D _rb;
    
    // private shit (to use for Maths)
    private float _targetRadius;
    private float _maxRadius;

    // Start is called before the first frame update
    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _player = _enemy.player;
        _rb = GetComponent<Rigidbody2D>();
        
        // randomize target distance
        _targetRadius = ((Random.value - .5f) * targetRadiusVariance + targetRadius);
        _maxRadius = _targetRadius - .5f;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float distance = (_player.position - transform.position).magnitude;
        
        // Clamp at -0.5f so enemies back away from player
        float speedMultiplier = Mathf.Clamp((distance - _maxRadius) / _targetRadius, -0.5f, 1);
        
        _rb.MovePosition(_rb.position + _enemy.VectorToPlayer().normalized * (speedMultiplier * speed * Time.deltaTime));
    }

    public bool AtTargetDistance()
    {
        // check if at target distance
        return (_player.position - transform.position).magnitude < _targetRadius;
    }
    
}
