using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour
{
    public static SendToGoogle Instance { get; private set; } // Singleton instance

    [SerializeField] private string URL = "https://docs.google.com/forms/u/0/d/1xZyN9kskQieYopl92nBkjy2guu_T_0tS9aRd8ruMxlg/formResponse";

    private long _sessionID;
    //private int _testInt;
    //private bool _testBool;
    //private float _testFloat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    //private void Awake() {
    //    _sessionID = DateTime.Now.Ticks;

    //    //Send();
    //}

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

    public void Send(long timeZombieKilled) {
        //_testInt = UnityEngine.Random.Range(0, 101);

        //_testBool = true;
        //_testFloat = UnityEngine.Random.Range(0.0f, 10.0f);

        //StartCoroutine(Post(_sessionID.ToString(), _testInt.ToString(), _testBool.ToString(), _testFloat.ToString()));
        //StartCoroutine(Post(_sessionID.ToString(), AnalyticsCommon.timeZombieKilled.ToString()));
        StartCoroutine(Post(_sessionID.ToString(), timeZombieKilled.ToString()));
    }

    //private IEnumerator Post(string sessionID, string testInt, string testBool, string testFloat)
    private IEnumerator Post(string sessionID, string timeZombieKilled)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.15244743", sessionID);
        form.AddField("entry.251709568", timeZombieKilled);
        //form.AddField("entry.1284425705", testBool);
        //form.AddField("entry.1132036655", testFloat);

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
