using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class HighscoreManager : MonoBehaviour
{
    // TODO: add real highscore server URI
    private readonly String HIGHSCORES_API_URI = "https://www.google.com"; 
    
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
                GameObject entry = Instantiate(highscoreEntry, transform);
                var component = entry.GetComponent<HighscoreEntry>();
                component._nameText.text = highscore.name;
                component._scoreText.text = highscore.score;
            }
        }
    }

    // TODO: add request to real highscore server
    private IEnumerator GetHighscores()
    {
        
        using (var request = UnityWebRequest.Get(HIGHSCORES_API_URI))
        {
            _highscores_loading = true;
            yield return request.SendWebRequest();

            Debug.Log(request.isNetworkError
                ? $"[HighscoreManager] Error: {request.error}"
                : $"[HighscoreManager] Received: {request.downloadHandler.text}");

//            _highscores = JsonUtility.FromJson<Highscore[]>(request.downloadHandler.text);
            var response = JsonUtility.FromJson<Response>(@"
{
    ""data"": [
        {
            ""name"": ""Markus"",
            ""score"": ""20""
        }    
    ]
}
            ");
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
