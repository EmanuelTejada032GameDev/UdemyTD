using UnityEngine;

public class BuildingGhostCursor : MonoBehaviour
{
    [SerializeField] Transform _buildingGhostTransform;
    [SerializeField] BuildingEfficiencyBasedOnNearbyResourceNodesOverlay _buildingEfficiencyBasedOnNearbyResourceNodesOverlay;


    private void Start()
    {
        BuildingManager.Instance.onBuildingTypeChange += OnBuildingTypeChange;
    }

    private void OnBuildingTypeChange(object sender, BuildingManager.BuildingTypeChangeEventArgs e)
    {
        _buildingGhostTransform.Find("sprite").gameObject.GetComponent<SpriteRenderer>().sprite = e.selectedBuildingType == null? null : e.selectedBuildingType.sprite;
        if(e.selectedBuildingType != null) {
            _buildingEfficiencyBasedOnNearbyResourceNodesOverlay.Show(e.selectedBuildingType.resourgeGenerationConfig);
        }
        else
        {
            _buildingEfficiencyBasedOnNearbyResourceNodesOverlay.Hide();
        }
    }

    private void Update()
    {
        transform.position = Utils.CursorScreenPosition();
    }
}
