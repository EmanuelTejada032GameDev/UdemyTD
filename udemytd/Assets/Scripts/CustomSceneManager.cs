using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public enum Scene
    {
        Game, 
        MainMenu
    }

    public static void Load(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
        Time.timeScale = 1f;
    }
}
