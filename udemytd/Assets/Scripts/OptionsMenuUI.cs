using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{

    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private MusicManager _musicManager;
    private TextMeshProUGUI _soundValueText;
    private TextMeshProUGUI _musicValueText;

    private void Awake()
    {
        _soundValueText = transform.Find("soundOption").Find("valueText").GetComponent<TextMeshProUGUI>();
        _musicValueText = transform.Find("musicOption").Find("valueText").GetComponent<TextMeshProUGUI>();

        transform.Find("soundOption").Find("increaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.Instance.IncreaseVolume();
            UpdateText();
        });

        transform.Find("soundOption").Find("decreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            SoundManager.Instance.DecreaseVolume();
            UpdateText();
        });

        transform.Find("musicOption").Find("increaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            MusicManager.Instance.IncreaseVolume();
            UpdateText();
        });

        transform.Find("musicOption").Find("decreaseBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            MusicManager.Instance.DecreaseVolume();
            UpdateText();
        });

        transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            CustomSceneManager.Load(CustomSceneManager.Scene.MainMenu);
        });

        transform.Find("toggleEdgeScrollingBtn").GetComponent<Toggle>().onValueChanged.AddListener((bool newValue) =>
        {
            CameraHandler.Instance.SetEdgeScrolling(newValue);
        });

    }

    private void Start()
    {
        UpdateText();
        gameObject.SetActive(false);
        transform.Find("toggleEdgeScrollingBtn").GetComponent<Toggle>().SetIsOnWithoutNotify(CameraHandler.Instance.GetScrollingValue());

    }

    private void Update()
    {
        ListenPauseBtns();
    }

    private void UpdateText()
    {
        _soundValueText.SetText(Mathf.RoundToInt(_soundManager.GetVolume() * 10).ToString());
        _musicValueText.SetText(Mathf.RoundToInt(_musicManager.GetVolume() * 10).ToString());
    }

    private void ListenPauseBtns()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ToggleMenuVisible();
        }
    }

    public void ToggleMenuVisible()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if (gameObject.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
