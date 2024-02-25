using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{

    public static BuildingManager Instance { get; private set; } 

    private Camera mainCamera;
    private BuildingTypeSO _selectedBuildingType;
    private List<BuildingTypeSO> buildingTypeSOList;

    public EventHandler<BuildingTypeChangeEventArgs> onBuildingTypeChange;

    public class BuildingTypeChangeEventArgs: EventArgs
    {
        public BuildingTypeSO selectedBuildingType;
    }

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
            if(_selectedBuildingType != null)
                Instantiate(_selectedBuildingType.pfHarvester, Utils.CursorScreenPosition(), Quaternion.identity);
        }
    }

    public void SetSelectedBuildingType(BuildingTypeSO buildingTypeSO)
    {
        _selectedBuildingType = buildingTypeSO;
        onBuildingTypeChange?.Invoke(this, new BuildingTypeChangeEventArgs { selectedBuildingType = buildingTypeSO});
    }

    public BuildingTypeSO GetSelectedBuildingType()
    {
        return _selectedBuildingType;
    }
}
