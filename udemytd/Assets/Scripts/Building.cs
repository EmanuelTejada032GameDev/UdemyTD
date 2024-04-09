using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private BuildingTypeSO _buildingType;

    public void Start()
    {
        _healthSystem = GetComponent<HealthSystem>();
        _buildingType = GetComponent<BuildingTypeHolder>().BuildingTypeSO;
        _healthSystem.OnDied += OnDied;
        _healthSystem.SetMaxHealthAmount(_buildingType.maxHealthAmount, true);
    }
    private void Update()
    {
    }

    private void OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }
}
