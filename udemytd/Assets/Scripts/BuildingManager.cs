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
    public LayerMask BuildingsLayerMask;

    [SerializeField] private bool _showAreaMaxDistanceToPlaceSelectedBuilding;

    public class BuildingTypeChangeEventArgs: EventArgs
    {
        public BuildingTypeSO selectedBuildingType;
    }

    [SerializeField] private Building _hqBuilding;

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
            if (_selectedBuildingType != null && CanPlaceBuilding(_selectedBuildingType, Utils.CursorScreenPosition())){
                if (ResourceManager.Instance.CanAffordBuilding(_selectedBuildingType.buildingResourceAmountCost))
                {
                    Instantiate(_selectedBuildingType.prefab, Utils.CursorScreenPosition(), Quaternion.identity);
                }
                else
                {
                    TooltipUI.Instance.Show($"Cannot Afford Building {_selectedBuildingType.BuildingCostStringMessage()}", new TooltipUI.TooltipTimer());
                }
            }
               
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

    public bool CanPlaceBuilding(BuildingTypeSO buildingTypeSO, Vector3 position)
    {
        // Check building on top of another object or any obstacle with a collider
        BoxCollider2D boxCollider2D = buildingTypeSO.prefab.GetComponent<BoxCollider2D>();
        Collider2D[] foundCollidersWithinColliderRadius = Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);
        bool isAreaClear = foundCollidersWithinColliderRadius.Length == 0;
        if (!isAreaClear)
        {
            TooltipUI.Instance.Show("Area is not clear", new TooltipUI.TooltipTimer());
            return false;
        }


        // Check building radius construction required to build is clear
        Collider2D[] foundCollidersWithinConstructionRequiredRadius = Physics2D.OverlapCircleAll(position, buildingTypeSO.spaceRadiusRequiredToBuild);
        foreach (Collider2D collider in foundCollidersWithinConstructionRequiredRadius)
        {
            BuildingTypeHolder buildingTypeHolder = collider.gameObject.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder == default) continue;
            if (buildingTypeHolder.BuildingTypeSO == buildingTypeSO)
            {
                TooltipUI.Instance.Show($"Too close to another {buildingTypeSO.soName}", new TooltipUI.TooltipTimer());
                return false;
            }

        }

        // Check building is not too far from other buildings
        Collider2D[] foundCollidersWithinMaxRadiusRequiredFromOtherBuildings = Physics2D.OverlapCircleAll(position, buildingTypeSO.maxDistanceFromOtherBuilding, BuildingsLayerMask);

        if (foundCollidersWithinMaxRadiusRequiredFromOtherBuildings.Length == 0)
        {
            TooltipUI.Instance.Show("Too far from other buildings", new TooltipUI.TooltipTimer());
            return false;
        }
        return true;
    }


    //private void OnDrawGizmos()
    //{
    //    if(_selectedBuildingType != null & _showAreaMaxDistanceToPlaceSelectedBuilding)
    //    {
    //        Gizmos.color = Color.white;
    //        Gizmos.DrawWireSphere(Utils.CursorScreenPosition(), _selectedBuildingType.maxDistanceFromOtherBuilding);
    //    }
    //}

    public Building GetHQBuilding() => _hqBuilding;

}
