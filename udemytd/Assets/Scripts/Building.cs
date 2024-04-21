using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private BuildingTypeSO _buildingType;
    [SerializeField] private Transform _buildingDemolishBtn;

    private void Awake()
    {
        _healthSystem = GetComponent<HealthSystem>();
    }

    public void Start()
    {
        _buildingType = GetComponent<BuildingTypeHolder>().BuildingTypeSO;
        _healthSystem.OnDied += OnDied;
        _healthSystem.SetMaxHealthAmount(_buildingType.maxHealthAmount, true);
        HideDemolishBtn();
    }

    private void Update()
    {
    }

    private void OnDied(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    public HealthSystem HealthSystem { get { return _healthSystem; } }

    private void OnMouseEnter()
    {
        ShowDemolishBtn();
    }

    private void OnMouseExit()
    {
        HideDemolishBtn();
    }

    private void ShowDemolishBtn()
    {
        if (_buildingDemolishBtn != null)
        {
            _buildingDemolishBtn.gameObject.SetActive(true);
        }
    }

    private void HideDemolishBtn()
    {
        if (_buildingDemolishBtn != null)
        {
            _buildingDemolishBtn.gameObject.SetActive(false);
        }
    }
}


