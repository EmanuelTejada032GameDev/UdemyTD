using S = System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform _targetTransform;
    private Rigidbody2D _rigidBody2d;
    [SerializeField] private float _moveSpeed = 6f;

    private float _lookForTargetTimer;
    private float _lookForTargetMaxTime;

    [SerializeField] private HealthSystem _healthSystem;

    public static Enemy Create(Vector3 position)
    {
        Transform pfEnemy = Resources.Load<Transform>("pfEnemy");
        Transform enemyTrasnform = Instantiate(pfEnemy, position, Quaternion.identity);
        return enemyTrasnform.GetComponent<Enemy>();
    }

    private void Start()
    {
        _rigidBody2d = GetComponent<Rigidbody2D>();
        Building hqBuilding = BuildingManager.Instance.GetHQBuilding();
        _targetTransform = hqBuilding == null? null : hqBuilding.transform;
        _healthSystem.OnDied += OnDied;
        _healthSystem.OnDamaged += HealthSystem_OnDamaged;


    }

    private void Update()
    {
        HandleMovement();
        HandleTargeting();
    }


    private void OnDied(object sender, S.EventArgs e)
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDie);
        Instantiate(Resources.Load<Transform>("pfEnemyDieParticles"), transform.position, Quaternion.identity);
        CameraShakeComponent.Instance.ShakeCamera(1f, .1f);
        Destroy(gameObject);
    }

    private void HealthSystem_OnDamaged(object sender, System.EventArgs e)
    {
        Debug.Log("EnemyDamaged");
        CameraShakeComponent.Instance.ShakeCamera(2f, .15f);
        SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);
    }

    private void HandleMovement()
    {
        if (_targetTransform != null)
        {
            Vector3 moveDir = (_targetTransform.position - transform.position).normalized;
            _rigidBody2d.velocity = moveDir * _moveSpeed;
        }
        else
        {
            LookForTargets();
        }
    }

    private void HandleTargeting()
    {
        _lookForTargetTimer -= Time.deltaTime;
        if (_lookForTargetTimer < 1f)
        {
            _lookForTargetTimer += _lookForTargetMaxTime;
            LookForTargets();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();

        if (building != default)
        {
            building.HealthSystem.TakeDamage(3);
            _healthSystem.TakeDamage(999);
        }
    }


    private void LookForTargets()
    {
        float _maxRadiusTargetDetection = 10f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, _maxRadiusTargetDetection);

        foreach (Collider2D collider2D in collider2DArray)
        {
            Building building = collider2D.gameObject.GetComponent<Building>();
            if(building != null)
            {
                if(_targetTransform == null)
                {
                    _targetTransform = building.transform;
                }
                else
                {
                    if(Vector3.Distance(transform.position, building.transform.position) < Vector3.Distance(transform.position, _targetTransform.position))
                    {
                        _targetTransform = building.transform;
                    }
                }
            }
        }

        if(_targetTransform == null)
        {

            Building hqBuilding = BuildingManager.Instance.GetHQBuilding();
            if (hqBuilding == null)
            {
                _rigidBody2d.velocity = Vector3.zero;
                SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyHit);
                SoundManager.Instance.PlaySound(SoundManager.Sound.EnemyDie);
                Destroy(gameObject, Random.Range(2.5f, 6f));
                return;
            }

            _targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
        }
    }
}
