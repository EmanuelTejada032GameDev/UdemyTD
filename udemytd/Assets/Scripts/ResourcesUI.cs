using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{
    List<ResourceTypeSO> _resourceTypes;
    Transform _resourceTemplateTransform;

    Dictionary<ResourceTypeSO, Transform> _resourceTypesTransformDictionary;

    void Awake()
    {
        _resourceTypes = Resources.Load<ResourceTypeSOList>("ResourceTypeSOList").soList;
        _resourceTemplateTransform = transform.Find("resourceTemplate");
        _resourceTemplateTransform.gameObject.SetActive(false);
        _resourceTypesTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();
    }

    private void Start()
    {
        ResourceManager.Instance.OnResourceAmountChanged += ResourceAmountChanged;
        IntializeResourceUI();
    }

    private void ResourceAmountChanged(object sender, EventArgs e)
    {
       UpdateResourceUI();
    }

    private void IntializeResourceUI()
    {
        int index = 0;
        float offsetPosition = -130f;

        foreach (ResourceTypeSO resourceTypeSO in _resourceTypes)
        {
            Transform resourceTransform = Instantiate(_resourceTemplateTransform, transform);
            resourceTransform.gameObject.SetActive(true);
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offsetPosition * index, 0);
            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceTypeSO.sprite;
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(ResourceManager.Instance.GetResourceAmount(resourceTypeSO).ToString());
            _resourceTypesTransformDictionary[resourceTypeSO] = resourceTransform;
            index++;
        }
    }

    private void UpdateResourceUI()
    {
        foreach (ResourceTypeSO resourceTypeSO in _resourceTypesTransformDictionary.Keys)
        {
            _resourceTypesTransformDictionary[resourceTypeSO].Find("text").GetComponent<TextMeshProUGUI>().SetText(ResourceManager.Instance.GetResourceAmount(resourceTypeSO).ToString());
        }
    }

}
