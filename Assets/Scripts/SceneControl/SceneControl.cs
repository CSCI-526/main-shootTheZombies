using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWithTutorial(){
        SceneManager.LoadScene(1);
    }
    public void StartWithoutTutorial(){
        SceneManager.LoadScene(2);
    }

    public void Ranks(){
        // SceneManager.LoadScene(3);
    }

}
