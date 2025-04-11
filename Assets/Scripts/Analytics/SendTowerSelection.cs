using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Windows;

public class SendTowerSelection : MonoBehaviour
{
    public static SendTowerSelection Instance { get; private set; } // Singleton instance

    [SerializeField] private string URL = "https://docs.google.com/forms/u/0/d/11sASLWG-F7JJ3BVdkZ15QIc7pOL-KVFgRGa_RU_x-ZY/formResponse";

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

    public void Send(long timeTowerSelection, string typeTowerSelection) {
        StartCoroutine(Post(_sessionID.ToString(), timeTowerSelection.ToString(), typeTowerSelection));
    }

    //private IEnumerator Post(string sessionID, string testInt, string testBool, string testFloat)
    private IEnumerator Post(string sessionID, string timeTowerSelection, string typeTowerSelection)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.528463348", sessionID);
        form.AddField("entry.1596094451", timeTowerSelection);
        form.AddField("entry.195736719", typeTowerSelection);

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
