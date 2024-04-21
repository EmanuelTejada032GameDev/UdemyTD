using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        transform.Find("tryAgainBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            CustomSceneManager.Load(CustomSceneManager.Scene.Game);
        });

        transform.Find("mainMenuBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            CustomSceneManager.Load(CustomSceneManager.Scene.MainMenu);
        });

        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        transform.Find("wavesSurvivedText").GetComponent<TextMeshProUGUI>().SetText("You survived "
            + (EnemyWaveSpawner.Instance.GetCurrentWave()-1).ToString() + " waves");
    }

    private void Hide()
    {
        gameObject.SetActive(false);

    }

}
