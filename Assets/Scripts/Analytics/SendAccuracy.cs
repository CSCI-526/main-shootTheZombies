using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Windows;

public class SendAccuracy : MonoBehaviour
{
    public static SendAccuracy Instance { get; private set; } // Singleton instance

    [SerializeField] private string URL = "https://docs.google.com/forms/u/0/d/1H-61x80dHdRw5jXSHYHhLMqBbV6uVPKvH3IMNHVxY6g/formResponse";
    [SerializeField] private float sendInterval = 5f; // How often to send data (in seconds)

    private long _sessionID;
    private float _timer = 0f;

    private long timeAccuracySent;

    public static int bulletsFired = 0;
    public static int bulletsHit = 0;

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

    public void Send(float accuracy) {
        StartCoroutine(Post(_sessionID.ToString(), accuracy.ToString()));
    }

    //private IEnumerator Post(string sessionID, string testInt, string testBool, string testFloat)
    private IEnumerator Post(string sessionID, string accuracy)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.35637537", sessionID);
        form.AddField("entry.1118204793", timeAccuracySent.ToString());
        form.AddField("entry.914810376", accuracy);

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
        _timer += Time.deltaTime;
        if (_timer >= sendInterval)
        {
            _timer = 0f; // Reset timer

            float accuracy = 0;
            if (bulletsFired > 0)
            {
                accuracy = (float)bulletsHit / bulletsFired;
            }

            timeAccuracySent = DateTime.Now.Ticks;
            Send(accuracy);
        }

    }
}
