using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    private List<BuildingTypeSO> _buildingTypeSOList;
    Transform _buildingTypeBtnTemplateTransform;
    Dictionary<BuildingTypeSO, Transform> _buildingTypeBtnTemplateTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

    [SerializeField] Transform _cursorTransformTemplate;
    Transform _cursorTransform;

    private void Awake()
    {
        _buildingTypeBtnTemplateTransform = transform.Find("btnTemplate");
        _buildingTypeBtnTemplateTransform.gameObject.SetActive(false);

        _buildingTypeSOList = Resources.Load<BuildingTypeSOList>(typeof(BuildingTypeSOList).Name).soList;
    }

    private void Start()
    {
        InitializeBuildingTypeUI();
    }

    private void InitializeBuildingTypeUI()
    {
        int index = 0;
        float offsetPosition = 110f;

        //set cursor btnTemplate first
        _cursorTransform = Instantiate(_cursorTransformTemplate, transform);
        _cursorTransform.gameObject.SetActive(true);
        _cursorTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetPosition * index, 0);
        _cursorTransform.gameObject.GetComponent<Button>().onClick.AddListener(() => SelectBuildingType(null));
        
        index++;

        foreach (BuildingTypeSO buildingTypeSO in _buildingTypeSOList)
        {
            Transform buildingTypeTransform = Instantiate(_buildingTypeBtnTemplateTransform, transform);
            buildingTypeTransform.gameObject.SetActive(true);
            buildingTypeTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetPosition * index, 0);
            buildingTypeTransform.Find("itemImage").GetComponent<Image>().sprite = buildingTypeSO.sprite;

            buildingTypeTransform.gameObject.GetComponent<Button>().onClick.AddListener(() => SelectBuildingType(buildingTypeSO));
            _buildingTypeBtnTemplateTransformDictionary[buildingTypeSO] = buildingTypeTransform;
            index++;
        }

        SelectBuildingType(null);
    }

    private void SelectBuildingType(BuildingTypeSO? buildingTypeSO)
    {
        BuildingManager.Instance.SetSelectedBuildingType(buildingTypeSO);

        _cursorTransform.Find("selected").gameObject.SetActive(false);
        foreach (BuildingTypeSO buildingType in _buildingTypeSOList)
        {
            _buildingTypeBtnTemplateTransformDictionary[buildingType].Find("selected").gameObject.SetActive(false);
        }

        if(BuildingManager.Instance.GetSelectedBuildingType() == null)
        {
            _cursorTransform.Find("selected").gameObject.SetActive(true);
        }
        else
        {   
            _buildingTypeBtnTemplateTransformDictionary[buildingTypeSO].Find("selected").gameObject.SetActive(true);
        }
    }
}
