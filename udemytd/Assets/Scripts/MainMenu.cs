using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        transform.Find("playBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            CustomSceneManager.Load(CustomSceneManager.Scene.Game);
        });

        transform.Find("quitBtn").GetComponent<Button>().onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
