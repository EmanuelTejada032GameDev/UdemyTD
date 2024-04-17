using UnityEngine;

public class Tower : MonoBehaviour
{
    private Enemy _targetTransform;
    [SerializeField] private Transform _shootPoint;

    private float _lookForTargetTimer;
    private float _lookForTargetMaxTime;

    private float _lastTimeShot;
    private float _maxTimeBetweenShots = 0.2f;


    private void Start()
    {
    }

    private void Update()
    {
        HandleTargeting();
        HandleShoot();
    }

    private void HandleShoot()
    {
        if (_targetTransform != null)
        {
            _lastTimeShot -= Time.deltaTime;
            if (_lastTimeShot < 1f)
            {
                _lastTimeShot += _maxTimeBetweenShots;
                ArrowProjectile.Create(_shootPoint.position,_targetTransform);
            }
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


    private void LookForTargets()
    {
        float _maxRadiusTargetDetection = 25f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, _maxRadiusTargetDetection);

        foreach (Collider2D collider2D in collider2DArray)
        {
            Enemy enemy = collider2D.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (_targetTransform == null)
                {
                    _targetTransform = enemy;
                }
                else
                {
                    if (Vector3.Distance(transform.position, enemy.transform.position) < Vector3.Distance(transform.position, _targetTransform.transform.position))
                    {
                        _targetTransform = enemy;
                    }
                }
            }
        }
    }
}
