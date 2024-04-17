using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType/Harvester")]
public class BuildingTypeSO : ScriptableObject
{
    public string soName;
    public Transform prefab;
    public Sprite sprite;
    public ResourceGeneratorConfig resourgeGenerationConfig;
    public float spaceRadiusRequiredToBuild;
    public float maxDistanceFromOtherBuilding;
    public float constructionTime;
    public int maxHealthAmount;
    public ResourceAmount[] buildingResourceAmountCost;
    public bool isResourceGeneratorBuilding;

    public string BuildingCostStringMessage()
    {
        string tooltipMessage = "";

        foreach (ResourceAmount requiredResource in buildingResourceAmountCost)
        {
            tooltipMessage += $"<color={requiredResource.resourceType.soNameHexColorDisplay}>{requiredResource.resourceType.shortSoName}{requiredResource.resourceAmount}</color> ";
        }
        return tooltipMessage;
    }

    //public List<ResourceGeneratorConfig> resourgeGenerationConfigList;
}
