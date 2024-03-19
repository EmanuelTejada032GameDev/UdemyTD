using TMPro;
using UnityEngine;

public class BuildingEfficiencyBasedOnNearbyResourceNodesOverlay : MonoBehaviour
{
    private ResourceGeneratorConfig _resourceGeneratorConfig;
    [SerializeField] private SpriteRenderer _resourceTypeSpriteRender;
    [SerializeField] private TextMeshPro _resourceAmountPerSecondText;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        int nearbyResourceNodes = ResourceGenerator.GetNearbyResourceNodes(transform.position, _resourceGeneratorConfig);
        float percent = Mathf.RoundToInt((float)nearbyResourceNodes / _resourceGeneratorConfig.MaxResourceNodes * 100f);
        _resourceAmountPerSecondText.SetText(percent + "%");
    }

    public void Show(ResourceGeneratorConfig resourceGeneratorConfig)
    {
        _resourceGeneratorConfig = resourceGeneratorConfig;
        _resourceTypeSpriteRender.sprite = _resourceGeneratorConfig.Resource.sprite;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
