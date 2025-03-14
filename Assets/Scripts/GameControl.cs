using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameControll : MonoBehaviour
{
    public GameObject restartButton;
    public Button pauseButton;
    public TextMeshProUGUI pauseButtonText;
    public Wall wall;

    private bool isPaused = false;

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (wall.health <= 0)
        {
            ShowRestartButton();
        }
    }

    public void RestartGame()
    {   
        //Debug.Log("Restart Game clicked~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Time.timeScale = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        //Debug.Log("Pause Game clicked!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;  
        pauseButtonText.text = isPaused ? "Resume" : "Pause";
    }

    private void ShowRestartButton()
    {
        restartButton.SetActive(true);
        pauseButton.interactable = false;
        Time.timeScale = 0;
    }
}
