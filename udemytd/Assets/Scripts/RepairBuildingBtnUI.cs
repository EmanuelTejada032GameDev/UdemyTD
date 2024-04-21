using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class RepairBuildingBtnUI : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private HealthSystem _healthSystem;
    [SerializeField] private ResourceTypeSO _repairResource;

    private void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            RepairBuilding();
        });
    }

    private void RepairBuilding()
    {
        int missingHealth = _healthSystem.MaxHealth - _healthSystem.HealthAmount;
        int repairCost = missingHealth / 2;

        ResourceAmount[] resourceAmount = new ResourceAmount[] {
            new ResourceAmount { resourceAmount = repairCost, resourceType = _repairResource }
        };


        if (ResourceManager.Instance.CanAffordResources(resourceAmount))
        {
            _healthSystem.HealFull();
            ResourceManager.Instance.SpendResources(resourceAmount);
        }
        else
        {
            TooltipUI.Instance.Show("cannot afford to repair building", new TooltipUI.TooltipTimer { Timer = 2f});
        }
    }
}
