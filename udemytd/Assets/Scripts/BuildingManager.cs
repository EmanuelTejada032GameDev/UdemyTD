using System.Collections.Generic;
using UnityEngine;


public class BuildingManager : MonoBehaviour
{
  
    private Camera mainCamera;
    private BuildingTypeSO buildingType;
    private List<BuildingTypeSO> buildingTypeSOList;

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
            Debug.Log("Left Click");
            buildingType = buildingTypeSOList[0];
            Instantiate(buildingType.pfHarvester, CursorScreenPosition(), Quaternion.identity);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Rigth Click");
            buildingType = buildingTypeSOList[1];
            Instantiate(buildingType.pfHarvester, CursorScreenPosition(), Quaternion.identity);
        }else if (Input.GetMouseButtonDown(2))
        {
            Debug.Log("Middle Click");
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
