using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public StartButton startButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnEnable()
    {
        startButton.OnStartButtonClicked += StartGame;
    }
    void OnDisable()
    {
        startButton.OnStartButtonClicked -= StartGame;
    }
    void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
