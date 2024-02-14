using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;
    private List<ResourceTypeSO> resourceTypeSoList;

    private void Awake()
    {
        Instance = this;
        InitializeResourceDictionary();
    }

    private void Update()
    {
       
    }

    private void PrintResourceDictionaryStatus()
    {
        foreach (ResourceTypeSO item in resourceAmountDictionary.Keys)
        {
            Debug.Log(item.soName + ": " + resourceAmountDictionary[item]);
        }
    }


    public void AddResource(ResourceTypeSO resourceTypeSO, int amount = 1)
    {
        resourceAmountDictionary[resourceTypeSO] += amount;
        PrintResourceDictionaryStatus();
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
