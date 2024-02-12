using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/BuildingType/Harvester")]
public class BuildingTypeSO : ScriptableObject
{
    [SerializeField] private string soName;
    public Transform pfHarvester;
}
