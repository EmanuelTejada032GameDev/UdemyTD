using System.Collections.Generic;
using UnityEngine;


public class BuildingManager : MonoBehaviour
{

    private Camera mainCamera;
    private BuildingTypeSO buildingType;
    private List<BuildingTypeSO> buildingTypeSOList;

    private void Awake()
    {
    }

    private void Start()
    {
        mainCamera = Camera.main;
        buildingTypeSOList = Resources.Load<BuildingTypeSOList>("BuildingTypeSOList").soList;
        buildingType = buildingTypeSOList[0];
    }

    private void Update()
    {
        //mouseCursorTransform.position = CursorScreenPosition();

        if (Input.GetMouseButtonDown(0))
        {
            buildingType = buildingTypeSOList[0];
            Instantiate(buildingType.pfHarvester, CursorScreenPosition(), Quaternion.identity);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            buildingType = buildingTypeSOList[1];
            Instantiate(buildingType.pfHarvester, CursorScreenPosition(), Quaternion.identity);
        }else if (Input.GetMouseButtonDown(2))
        {
            buildingType = buildingTypeSOList[2];
            Instantiate(buildingType.pfHarvester, CursorScreenPosition(), Quaternion.identity);
        }
    }

    private Vector3 CursorScreenPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;   
        return mouseWorldPosition;
    }
}
