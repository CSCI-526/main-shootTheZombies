using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject restartButton;
    public Button pauseButton;
    public TextMeshProUGUI pauseButtonText;
    public Wall wall;

    private bool isPaused = false;

    private void Update()
    {
        if (wall.health <= 0)
        {
            ShowRestartButton();
            //Debug.Log("restart button!");
        }
    }

    private void Start()
    {
        Time.timeScale = 1;  
    }

    public void RestartGame()
    {   
        //Debug.Log("Restart Game clicked~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PauseGame()
    {
        //Debug.Log("Pause Game clicked!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;  //1 resume , 0 pause
        pauseButtonText.text = isPaused ? "Resume" : "Pause";
    }

    private void ShowRestartButton()
    {
        //Debug.Log("in restart button!");
        restartButton.SetActive(true);
        pauseButton.interactable = false;
        Time.timeScale = 0;
    }
}
