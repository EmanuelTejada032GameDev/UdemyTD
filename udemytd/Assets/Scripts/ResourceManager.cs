using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;
    private List<ResourceTypeSO> resourceTypeSoList;

    private void Awake()
    {
        InitializeResourceDictionary();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            AddResource(resourceTypeSoList[0], 2);
            PrintResourceDictionaryStatus();

        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            AddResource(resourceTypeSoList[1], 2);
            PrintResourceDictionaryStatus();

        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            AddResource(resourceTypeSoList[2], 2);
            PrintResourceDictionaryStatus();
        }
    }

    private void PrintResourceDictionaryStatus()
    {
        foreach (ResourceTypeSO item in resourceAmountDictionary.Keys)
        {
            Debug.Log(item.soName + ": " + resourceAmountDictionary[item]);
        }
    }


    public void AddResource(ResourceTypeSO resourceTypeSO, int amount)
    {
        resourceAmountDictionary[resourceTypeSO] += amount;
    }

    private void InitializeResourceDictionary()
    {
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        resourceTypeSoList = Resources.Load<ResourceTypeSOList>(nameof(ResourceTypeSOList)).soList;
        foreach (ResourceTypeSO resourceType in resourceTypeSoList)
        {
            resourceAmountDictionary[resourceType] = 0;
        }
    }
}
