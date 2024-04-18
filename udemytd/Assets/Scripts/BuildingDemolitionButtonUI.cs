
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolitionButtonUI : MonoBehaviour
{

    [SerializeField] private Button _button;
    [SerializeField] private Building _building;

    private void Awake()
    {
        _button.onClick.AddListener( () =>
        {
            ResourceAmount[] resourceCost = _building.gameObject.GetComponent<BuildingTypeHolder>().BuildingTypeSO.buildingResourceAmountCost;
            foreach (ResourceAmount resourceAmount in resourceCost)
            {
                ResourceManager.Instance.AddResource(resourceAmount.resourceType,Mathf.FloorToInt(resourceAmount.resourceAmount * 0.6f));
            }

            Destroy(_building.gameObject);
        });
    }


}
