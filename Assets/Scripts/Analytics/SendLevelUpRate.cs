using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Windows;

public class SendLevelUpRate : MonoBehaviour
{
    public static SendLevelUpRate Instance { get; private set; } // Singleton instance

    [SerializeField] private string URL = "https://docs.google.com/forms/u/0/d/1IpFpU7Lay32YuncERa6t60g2jDIUsmYLU22BrInMjdM/formResponse";

    private long _sessionID;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
            _sessionID = DateTime.Now.Ticks; // Set session ID once
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
        }
    }

    public void Send(long timeLevelUp) {
        StartCoroutine(Post(_sessionID.ToString(), timeLevelUp.ToString()));
    }

    //private IEnumerator Post(string sessionID, string testInt, string testBool, string testFloat)
    private IEnumerator Post(string sessionID, string timeLevelUp)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.975321052", sessionID);
        form.AddField("entry.1798516870", timeLevelUp);

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            } else
            {
                Debug.Log("form upload complete");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
