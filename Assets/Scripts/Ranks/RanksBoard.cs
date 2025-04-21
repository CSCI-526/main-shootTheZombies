using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Networking;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

public class RanksBoard : MonoBehaviour
{
    public UIDocument uiDocument;
    public string listViewName = "leaderboardList";

    private static string spreadsheetId = "1xnFEx2P40Xn6aegb9-rZ42ckEbF7EMFdiTsDszqtgek";
    private static string gid           = "1369200162";
    private string sheetCsvUrl = $"https://docs.google.com/spreadsheets/d/{spreadsheetId}/export?format=csv&gid={gid}";

    public class Entry
    {
        public float survivalTime;
        public string playerName;
        public string timestamp;
    }

    private List<Entry> entries = new List<Entry>();
    private ListView  listView;

    void OnEnable()
    {
        listView = uiDocument.rootVisualElement.Q<ListView>(listViewName);
        var btn = uiDocument.rootVisualElement.Q<Button>("backToMain");
        btn.clicked += backToMain;
        StartCoroutine(FetchCsv());
    }

    void backToMain(){
        SceneManager.LoadScene(0);
    }

    IEnumerator FetchCsv()
    {
        using (var www = UnityWebRequest.Get(sheetCsvUrl))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // Debug.LogError("下载失败: " + www.error);
                yield break;
            }

            ParseAndStore(www.downloadHandler.text);
            BindAndRefreshListView();
        }
    }

    void ParseAndStore(string csv)
    {
        entries.Clear();
        var lines = csv.Split('\n');
        int topTen = math.min(5, lines.Length); // top 5 records
        for (int i = 1; i <= topTen; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;
            var cols = lines[i].Split(',');
            if (cols.Length < 2) continue;

            if (float.TryParse(cols[1], out float t))
                entries.Add(new Entry { playerName = cols[2], survivalTime = t, timestamp = cols[0] });
        }
    }

    void BindAndRefreshListView()
    {
        listView.makeItem  = () => new Label();
        listView.bindItem  = (ve, idx) =>
        {
            var e = entries[idx];
            // ((Label)ve).text = $"{idx+1}.  {e.survivalTime:F2}s — {e.playerName} - {e.timestamp}";
            ((Label)ve).text = $"{idx+1}.  {e.survivalTime:F2}s — {e.playerName}";
        };
        listView.itemsSource = entries;
        listView.Rebuild();    
    }
}
