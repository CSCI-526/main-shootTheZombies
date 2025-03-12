using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SendToGoogle : MonoBehaviour
{
    [SerializeField] private string URL;

    private long _sessionID;
    private int _testInt;
    private bool _testBool;
    private float _testFloat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake() {
        _sessionID = DateTime.Now.Ticks;

        Send();
    }

    public void Send() {
        _testInt = UnityEngine.Random.Range(0, 101);
        _testBool = true;
        _testFloat = UnityEngine.Random.Range(0.0f, 10.0f);

        StartCoroutine(Post(_sessionID.ToString(), _testInt.ToString(), _testBool.ToString(), _testFloat.ToString()));
    }

    private IEnumerator Post(string sessionID, string testInt, string testBool, string testFloat)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.15244743", sessionID);
        form.AddField("entry.251709568", testInt);
        form.AddField("entry.1284425705", testBool);
        form.AddField("entry.1132036655", testFloat);

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
