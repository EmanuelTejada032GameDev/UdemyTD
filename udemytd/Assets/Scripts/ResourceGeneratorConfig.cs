using UnityEngine;

[System.Serializable]
public class ResourceGeneratorConfig
{
    [SerializeField] private float _timeToGenerateResource;
    [SerializeField] private ResourceTypeSO _resource;
    [SerializeField] private float _resourceDetectionRadius;
    [SerializeField] private int _maxResourceNodes;


    public ResourceTypeSO Resource  => _resource;
    public float TimeToGenerateResource  => _timeToGenerateResource;
    public float ResourceDetectionRadius => _resourceDetectionRadius;
    public int MaxResourceNodes => _maxResourceNodes;
}