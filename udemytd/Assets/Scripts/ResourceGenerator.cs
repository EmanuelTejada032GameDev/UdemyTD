using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    private BuildingTypeSO _buildingTypeSO;

    private float _timer;
    [SerializeField] private float _timeToGenerateResource;

    private void Awake()
    {
        _buildingTypeSO = GetComponent<BuildingTypeHolder>().BuildingTypeSO;
        _timeToGenerateResource = _buildingTypeSO.resourgeGenerationConfig.TimeToGenerateResource;
    }

    void Update()
    {
        _timer -= Time.deltaTime;
        if(_timer <= 0f)
        {
            ResourceManager.Instance.AddResource(_buildingTypeSO.resourgeGenerationConfig.Resource);
            Debug.Log($"Generating 1 {_buildingTypeSO.resourgeGenerationConfig.Resource.soName}");
            _timer += _timeToGenerateResource;
        }
    }
}
