using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{

    public static BuildingManager Instance { get; private set; } 

    private Camera mainCamera;
    private BuildingTypeSO selectedBuildingType;
    private List<BuildingTypeSO> buildingTypeSOList;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        buildingTypeSOList = Resources.Load<BuildingTypeSOList>("BuildingTypeSOList").soList;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() )
        {
            if(selectedBuildingType != null)
                Instantiate(selectedBuildingType.pfHarvester, CursorScreenPosition(), Quaternion.identity);
        }
    }

    private Vector3 CursorScreenPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;   
        return mouseWorldPosition;
    }

    public void SetSelectedBuildingType(BuildingTypeSO buildingTypeSO)
    {
        selectedBuildingType = buildingTypeSO;
    }

    public BuildingTypeSO GetSelectedBuildingType()
    {
        return selectedBuildingType;
    }
}
