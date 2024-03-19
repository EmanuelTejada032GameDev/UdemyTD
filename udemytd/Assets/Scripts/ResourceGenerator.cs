using System;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratorConfig _resourceGeneratorConfig;

    private float _timer;
    [SerializeField] private float _timeToGenerateResource;
    private static int _reachedResourceNodes = 0;

    private void Start()
    {
       
    }

    private void Awake()
    {
        _resourceGeneratorConfig = GetComponent<BuildingTypeHolder>().BuildingTypeSO.resourgeGenerationConfig;
        _reachedResourceNodes = GetNearbyResourceNodes(transform.position, _resourceGeneratorConfig);

        SetTimeToGenerateResourceBasedOnReachedResourceNodes();
     
    }

    private void SetTimeToGenerateResourceBasedOnReachedResourceNodes()
    {
        if (_reachedResourceNodes == 0)
        {
            _timeToGenerateResource = 0;
            enabled = false;
            return;
        }

        // This logic to determine _timeTOGenerateResource is not like course class 17 , min 21
        float baseTime = _resourceGeneratorConfig.TimeToGenerateResource; // Base time for one unit of resource
        float scaleFactor = 1.2f; // Scaling factor to adjust time
        _timeToGenerateResource = baseTime / (1 + scaleFactor * _reachedResourceNodes);

        //this is 
        //_timeToGenerateResource = (_resourceGeneratorConfig.TimeToGenerateResource / 2f) + _resourceGeneratorConfig.TimeToGenerateResource * (1 - (float)_reachedResourceNodes / _resourceGeneratorConfig.MaxResourceNodes);
    }

    void Update()
    {
        GenerateResource();
    }

    private void GenerateResource()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _timer += _timeToGenerateResource;
            ResourceManager.Instance.AddResource(_resourceGeneratorConfig.Resource);
        }
    }

    public static int GetNearbyResourceNodes(Vector3 originPosition, ResourceGeneratorConfig resourceGeneratorConfig )
    {
        Collider2D[] collisionedObjects = Physics2D.OverlapCircleAll(originPosition, resourceGeneratorConfig.ResourceDetectionRadius);
        int nearbyResources = 0;

        foreach (Collider2D collisionedObject in collisionedObjects)
        {
            ResourceNode resourceNode = collisionedObject.GetComponent<ResourceNode>();
            if (resourceNode != default && resourceNode.ResourceType == resourceGeneratorConfig.Resource)
            {
                nearbyResources++;
            }
        }

        Mathf.Clamp(nearbyResources, 0, resourceGeneratorConfig.MaxResourceNodes);

        return nearbyResources;
    }

    public ResourceGeneratorConfig GetResourceGeneratorConfig()
    {
        return _resourceGeneratorConfig;
    }

    public float GetTimeNormalized()
    {
        return _timeToGenerateResource == 0? 0: _timer / _timeToGenerateResource;
    }

    public float GetAmountGeneratedPerSecond()
    {
        return _timeToGenerateResource == 0? 0 : 1 / _timeToGenerateResource;
    }
}
