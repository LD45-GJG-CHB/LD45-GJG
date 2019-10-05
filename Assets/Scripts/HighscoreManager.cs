using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

public class HighscoreManager : MonoBehaviour
{
    private readonly String HIGHSCORES_API_URI = "http://167.99.142.75/game/scores"; 
    
    public GameObject highscoreEntry;
    private Highscore[] _highscores;
    private bool _highscores_loading = false;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetHighscores());
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        if (!_highscores_loading && _highscores.Length > 0)
        {
            foreach (var highscore in _highscores)
            {
                InstantiateEntry(highscore);
            }
        }
    }

    private void InstantiateEntry(Highscore highscore)
    {
        GameObject entry = Instantiate(highscoreEntry, transform);
        var component = entry.GetComponent<HighscoreEntry>();
        component._nameText.text = highscore.name;
        component._scoreText.text = highscore.score;
    }

    private IEnumerator GetHighscores()
    {
        
        Debug.Log($"[HighscoreManager] GET {HIGHSCORES_API_URI}");
        using (var request = UnityWebRequest.Get(HIGHSCORES_API_URI))
        {
            _highscores_loading = true;
            yield return request.SendWebRequest();

            Debug.Log(request.isNetworkError
                ? $"[HighscoreManager] Error: {request.error}"
                : $"[HighscoreManager] Received: {request.downloadHandler.text}");

            var response = JsonUtility.FromJson<Response>(request.downloadHandler.text);
            _highscores = response.data;
            _highscores_loading = false;
        }
    }

    [System.Serializable]
    public class Response
    {
        public Highscore[] data;
    }

    [System.Serializable]
    public class Highscore
    {
        public String name;
        public String score;
    }
}
