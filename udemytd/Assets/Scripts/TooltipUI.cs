
using System;
using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }

    [SerializeField] private RectTransform _canvasRectTransform; 

    private RectTransform _rectTransform;
    private TextMeshProUGUI _tooltipText;
    private RectTransform _backgroundRectTransform;

    private void Awake()
    {
        Instance = this;
        _tooltipText = transform.Find("text").GetComponent<TextMeshProUGUI>();
        _backgroundRectTransform = transform.Find("background").GetComponent<RectTransform>();
        _rectTransform = GetComponent<RectTransform>();

        Hide();
    }

    private void Update()
    {
        _rectTransform.anchoredPosition = Input.mousePosition / _canvasRectTransform.localScale.x; //  to get precise position since scale mode is screen with screen size
    }

    private void SetText(string newText)
    {
        _tooltipText.SetText(newText);
        _tooltipText.ForceMeshUpdate();

        Vector2 textSize = _tooltipText.GetRenderedValues(false);
        Vector2 tooltipBoxPadding = new Vector2(8,8);
        _backgroundRectTransform.sizeDelta = textSize + tooltipBoxPadding; 
    }

    public void Show(string newText)
    {
        SetText(newText);
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        SetText("");
    }
  
}
