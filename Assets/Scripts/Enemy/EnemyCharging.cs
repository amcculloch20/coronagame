using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyCharging : MonoBehaviour
{

    [Header("Movement")]
    public float turnSpeed = 3f;
    public float speed = .2f;
    public float drag = 2f;
    public float minChargeDistance = 2.5f;
    public float chargeSpeed = 5f;
    public float chargeDuration = 2f;
    public float lockTime = .5f;

    [Header("Damage")]
    public float damage = 15;

    // to auto-assign
    private Enemy _enemy;
    private Rigidbody2D _rb;
    
    // for Maths
    private string _chargeState = "NONE";
    private float _lastLock;
    private float _lastCharge;

    private Vector3 _velocity;

    void Start()
    {
        _enemy = GetComponent<Enemy>();
        _rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (_chargeState == "CHARGING")
        {
            
        }
        else
        {
            // turn towards player until we can charge
            Vector2 vToPlayer = _enemy.VectorToPlayer();
            float targetDeg = Mathf.Atan2(vToPlayer.y, vToPlayer.x) * Mathf.Rad2Deg - 90;
            Quaternion targetRotation = Quaternion.AngleAxis(targetDeg, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (_chargeState == "NONE")
        {
            // move forward when not locked
            _velocity += transform.up * (speed * Time.fixedDeltaTime);

            if (_enemy.VectorToPlayer().magnitude < minChargeDistance)
            {
                _chargeState = "LOCKED";
                _lastLock = Time.time;
            }
        }
        else if (_chargeState == "LOCKED")
        {
            if (Time.time - _lastLock > lockTime)
            {
                // charge if still in range, otherwise just go back to wandering
                if (_enemy.VectorToPlayer().magnitude < minChargeDistance + .5f)
                {
                    _chargeState = "CHARGING";
                    _lastCharge = Time.time;
                }
                else _chargeState = "NONE";
            }
        }
        else
        {
            // CHARGING BABEEE
            _velocity += transform.up * (chargeSpeed * Time.fixedDeltaTime);
            if (Time.time - _lastCharge > chargeDuration) _chargeState = "NONE";
        }
        
        _rb.MovePosition(transform.position + _velocity);
        // TODO: maybe this is a bit jank
        _velocity *= Mathf.Max(1 - Time.fixedDeltaTime * drag, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // damage damageable if charging
        if (_chargeState == "CHARGING")
        {
            Damageable damageable = other.gameObject.GetComponent<Damageable>();
            if (damageable)
            {
                damageable.Damage(damage);
            }
        }
    }
}
