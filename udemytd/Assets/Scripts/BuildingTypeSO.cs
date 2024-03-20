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
    public ResourceAmount[] buildingResourceAmountCost;

    //public List<ResourceGeneratorConfig> resourgeGenerationConfigList;
}
