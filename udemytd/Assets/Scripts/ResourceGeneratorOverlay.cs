using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator _resourceGenerator;
    [SerializeField] private SpriteRenderer _resourceTypeSpriteRender;
    [SerializeField] private TextMeshPro _resourceAmountPerSecondText;
    [SerializeField] private Transform _barTransform;

    private void Start()
    {
        _resourceTypeSpriteRender.sprite = _resourceGenerator.GetResourceGeneratorConfig().Resource.sprite;
        _resourceAmountPerSecondText.SetText(_resourceGenerator.GetAmountGeneratedPerSecond().ToString("f1"));
    }

    private void Update()
    {
        _barTransform.localScale = new Vector3(1-_resourceGenerator.GetTimeNormalized(),1,1);
    }
}
