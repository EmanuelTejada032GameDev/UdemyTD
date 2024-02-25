using UnityEngine;

public class BuildingGhostCursor : MonoBehaviour
{
    [SerializeField] Transform _buildingGhostTransform;


    private void Start()
    {
        BuildingManager.Instance.onBuildingTypeChange += OnBuildingTypeChange;
    }

    private void OnBuildingTypeChange(object sender, BuildingManager.BuildingTypeChangeEventArgs e)
    {
        _buildingGhostTransform.Find("sprite").gameObject.GetComponent<SpriteRenderer>().sprite = e.selectedBuildingType == null? null : e.selectedBuildingType.sprite;
    }

    private void Update()
    {
        transform.position = Utils.CursorScreenPosition();
    }
}
