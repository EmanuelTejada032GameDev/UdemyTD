using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform _targetTransform;
    private Rigidbody2D _rigidBody2d;
    [SerializeField] private float _moveSpeed = 6f;


    public static Enemy Create(Vector3 position)
    {
        Transform pfEnemy = Resources.Load<Transform>("pfEnemy");
        Transform enemyTrasnform = Instantiate(pfEnemy, position, Quaternion.identity);
        return enemyTrasnform.GetComponent<Enemy>();
    }

    private void Start()
    {
        _rigidBody2d = GetComponent<Rigidbody2D>();
        _targetTransform = BuildingManager.Instance.GetHQBuilding().transform;
    }

    private void Update()
    {
        Vector3 moveDir = (_targetTransform.position - transform.position).normalized;
        _rigidBody2d.velocity = moveDir * _moveSpeed;   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Building building = collision.gameObject.GetComponent<Building>();

        if (building != default)
        {
            building.HealthSystem.TakeDamage(6);
            Destroy(gameObject);
        }
    }
}
