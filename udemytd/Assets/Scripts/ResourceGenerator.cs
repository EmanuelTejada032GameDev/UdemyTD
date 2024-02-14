using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private BuildingTypeSO _buildingTypeSO;

    private Dictionary<ResourceGeneratorConfig, float> _timerDictionary;
    private Dictionary<ResourceGeneratorConfig, float> _timeToGenerateResourceDictionary;

    private void Awake()
    {
        _buildingTypeSO = GetComponent<BuildingTypeHolder>().BuildingTypeSO;
        _timerDictionary = new Dictionary<ResourceGeneratorConfig, float>();
        _timeToGenerateResourceDictionary = new Dictionary<ResourceGeneratorConfig, float>();

        foreach (ResourceGeneratorConfig resourceGeneratorConfig in _buildingTypeSO.resourgeGenerationConfigList)
        {
            _timerDictionary[resourceGeneratorConfig] = 0f;
            _timeToGenerateResourceDictionary[resourceGeneratorConfig] = resourceGeneratorConfig.TimeToGenerateResource;
        }
    }

    void Update()
    {
        foreach (ResourceGeneratorConfig resourceGeneratorConfig in _buildingTypeSO.resourgeGenerationConfigList)
        {
            _timerDictionary[resourceGeneratorConfig] -= Time.deltaTime;
            if (_timerDictionary[resourceGeneratorConfig] <= 0f)
            {
                ResourceManager.Instance.AddResource(resourceGeneratorConfig.Resource);
                _timerDictionary[resourceGeneratorConfig] += _timeToGenerateResourceDictionary[resourceGeneratorConfig];
            }
        }
    }

}
