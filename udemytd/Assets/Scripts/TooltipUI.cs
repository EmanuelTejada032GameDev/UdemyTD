
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
    private TooltipTimer _tooltipTimer;

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
        UpdateTooltipPosition();
    }

    private void UpdateTooltipPosition()
    {
        Vector2 anchoredPosition = Input.mousePosition / _canvasRectTransform.localScale.x; //  to get precise position since scale mode is screen with screen size

        if (anchoredPosition.x + _backgroundRectTransform.rect.width > _canvasRectTransform.rect.width)
        {
            anchoredPosition.x = _canvasRectTransform.rect.width - _backgroundRectTransform.rect.width;
        }

        if (anchoredPosition.y + _backgroundRectTransform.rect.height > _canvasRectTransform.rect.height)
        {
            anchoredPosition.y = _canvasRectTransform.rect.height - _backgroundRectTransform.rect.height;
        }

        _rectTransform.anchoredPosition = anchoredPosition;
        if (_tooltipTimer != null)
        {
            _tooltipTimer.Timer -= Time.deltaTime;
            if (_tooltipTimer.Timer <= 0) Hide();
        }

    }

    private void SetText(string newText)
    {
        _tooltipText.SetText(newText);
        _tooltipText.ForceMeshUpdate();

        Vector2 textSize = _tooltipText.GetRenderedValues(false);
        Vector2 tooltipBoxPadding = new Vector2(8,8);
        _backgroundRectTransform.sizeDelta = textSize + tooltipBoxPadding;
    }

    public void Show(string newText, TooltipTimer timer = null)
    {
        _tooltipTimer = timer;
        gameObject.SetActive(true);
        SetText(newText);
        UpdateTooltipPosition();
    }

    public void Hide()
    {
        SetText("");
        gameObject.SetActive(false);
    }

    public class TooltipTimer
    {
        public float Timer { get; set; }

        public TooltipTimer(float timerValue = 2f)
        {
            Timer = timerValue;
        }
    }

}
