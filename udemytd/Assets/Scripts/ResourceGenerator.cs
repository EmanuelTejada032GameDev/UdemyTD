using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private ResourceGeneratorConfig _resourceGeneratorConfig;

    private float _timer;
    [SerializeField] private float _timeToGenerateResource;
    private static int reachedResourceNodes = 0;

    private void Start()
    {
        reachedResourceNodes = GetNearbyResourceNodes(transform.position, _resourceGeneratorConfig);

        Mathf.Clamp(reachedResourceNodes, 0, _resourceGeneratorConfig.MaxResourceNodes);
        if (reachedResourceNodes == 0) enabled = false;

        float baseTime = _resourceGeneratorConfig.TimeToGenerateResource; // Base time for one unit of resource
        float scaleFactor = 1.2f; // Scaling factor to adjust time
        _timeToGenerateResource = baseTime / (1 + scaleFactor * reachedResourceNodes);
    }

    private void Awake()
    {
        _resourceGeneratorConfig = GetComponent<BuildingTypeHolder>().BuildingTypeSO.resourgeGenerationConfig;
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

        return nearbyResources;
    }

    public ResourceGeneratorConfig GetResourceGeneratorConfig()
    {
        return _resourceGeneratorConfig;
    }

    public float GetTimeNormalized()
    {
        return _timer / _timeToGenerateResource;
    }

    public float GetAmountGeneratedPerSecond()
    {
        return 1 / _timeToGenerateResource;
    }
}
