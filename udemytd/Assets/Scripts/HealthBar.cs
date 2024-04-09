using System;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem _healthSystem;

    private Transform _barTrasform;

    private void Awake()
    {
        _barTrasform = transform.Find("bar");
    }

    private void Start()
    {
        _healthSystem.OnDamaged += OnDamaged;
        UpdateBarTransform();
    }

    private void OnDamaged(object sender, EventArgs e)
    {
        UpdateBarTransform();
    }

    private void UpdateBarTransform()
    {
        if (_healthSystem.IsFullHealth()) gameObject.SetActive(false);
        else gameObject.SetActive(true);

        _barTrasform.localScale = new Vector3(_healthSystem.NormalizedHealthAmount, 1,1);
    }
}
