using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    [SerializeField] private ResourceTypeSO _resourceTypeSO;
    public ResourceTypeSO ResourceType => _resourceTypeSO;
}
