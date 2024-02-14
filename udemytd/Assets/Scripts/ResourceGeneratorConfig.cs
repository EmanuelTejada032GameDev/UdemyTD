using UnityEngine;

[System.Serializable]
public class ResourceGeneratorConfig
{
    [SerializeField] private float _timeToGenerateResource;
    [SerializeField] private ResourceTypeSO _resource;

    public ResourceTypeSO Resource  => _resource;
    public float TimeToGenerateResource  => _timeToGenerateResource;
}