using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType/Harvester")]
public class BuildingTypeSO : ScriptableObject
{
    public string soName;
    public Transform pfHarvester;
    public List<ResourceGeneratorConfig> resourgeGenerationConfigList;
}
