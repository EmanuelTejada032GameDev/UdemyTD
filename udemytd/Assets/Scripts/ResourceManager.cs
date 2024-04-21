using Mono.Cecil;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public event EventHandler OnResourceAmountChanged;

    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;

    [SerializeField]private List<ResourceAmount> startingResourceAmount;
    private List<ResourceTypeSO> resourceTypeSoList;

    private void Awake()
    {
        Instance = this;
        InitializeResourceDictionary();
    }

    private void Update()
    {
       
    }


    public void AddResource(ResourceTypeSO resourceTypeSO, int amount = 1)
    {
        resourceAmountDictionary[resourceTypeSO] += amount;
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
    }

    private void InitializeResourceDictionary()
    {
        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();
        resourceTypeSoList = Resources.Load<ResourceTypeSOList>(nameof(ResourceTypeSOList)).soList;
        foreach (ResourceTypeSO resourceType in resourceTypeSoList)
        {
            resourceAmountDictionary[resourceType] = 0;
        }

        SetStartResourcesAmount();
    }

    private void SetStartResourcesAmount()
    {
        foreach (ResourceAmount resourceaAmount in startingResourceAmount)
        {
            resourceAmountDictionary[resourceaAmount.resourceType] = resourceaAmount.resourceAmount;
        }
    }

    public int GetResourceAmount(ResourceTypeSO resourceTypeSO)
    {
        return resourceAmountDictionary[resourceTypeSO];
    }

    public bool CanAffordResources(ResourceAmount[] resourceAmountsCost)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountsCost)
        {
            if (resourceAmountDictionary[resourceAmount.resourceType] < resourceAmount.resourceAmount) return false;
        }
        return true;
    }

    public void SpendResources(ResourceAmount[] resourceAmountsCost)
    {
        foreach (ResourceAmount resourceAmount in resourceAmountsCost)
        {
            resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.resourceAmount;
        }
    }

   
}
