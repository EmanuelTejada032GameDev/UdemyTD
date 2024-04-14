using System;
using Unity.VisualScripting;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{

    private Enemy _enemyTarget;
    [SerializeField] private float _moveSpeed = 10f;

    private Vector3 _lastMoveDirection;

    private void Start()
    {
        Destroy(gameObject,2.5f);
    }

    public static ArrowProjectile Create(Vector3 position, Enemy enemyTarget)
    {
        Transform pfArrowProjectileTransform = Resources.Load<Transform>("pfArrowProjectile");
        Transform arrowProjectileTransform = Instantiate(pfArrowProjectileTransform, position, Quaternion.identity);
        arrowProjectileTransform.GetComponent<ArrowProjectile>().SetTarget(enemyTarget);
        return arrowProjectileTransform.GetComponent<ArrowProjectile>();
    }

    private void Update()
    {
     
            HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection;
        if (_enemyTarget != null)
        {
            moveDirection  = (_enemyTarget.transform.position - transform.position).normalized;
           _lastMoveDirection = moveDirection;
        }else
        {
            moveDirection = _lastMoveDirection;
        }

        transform.position += moveDirection * _moveSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, 0, Utils.GetEulerAngleFromVector3(moveDirection));

    }

    private void SetTarget(Enemy enemy)
    {
        _enemyTarget = enemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.GetComponent<HealthSystem>().TakeDamage(5);
            Destroy(gameObject);
        }
    }

}
