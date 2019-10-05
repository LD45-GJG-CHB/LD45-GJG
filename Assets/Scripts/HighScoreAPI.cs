using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class HighScoreAPI
{
    private static readonly String HIGHSCORES_API_URI = "http://167.99.142.75/game/scores";

    public static IEnumerator GetList(Action<string> callback)
    {
        var uuid = Guid.NewGuid();
        Debug.Log($"[HighscoreManager-{uuid.ToString()}] GET {HIGHSCORES_API_URI}");
        using (var request = UnityWebRequest.Get(HIGHSCORES_API_URI))
        {
            yield return request.SendWebRequest();

            Debug.Log(request.isNetworkError
                ? $"[HighscoreManager-{uuid.ToString()}] Error: {request.error}"
                : $"[HighscoreManager-{uuid.ToString()}] Received: {request.downloadHandler.text}");

            callback(request.downloadHandler.text);
        }
    }

    public static IEnumerator Save(string name, int score, Action<string> callback)
    {
        var uuid = Guid.NewGuid();
        Debug.Log($"[HighscoreManager-{uuid.ToString()}] POST {HIGHSCORES_API_URI}");

        var body = new SaveRequestBody(name, score);
        Debug.Log(JsonUtility.ToJson(body));
        using (var request = UnityWebRequest.Put(HIGHSCORES_API_URI, JsonUtility.ToJson(body)))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.method = UnityWebRequest.kHttpVerbPOST;
            yield return request.SendWebRequest();

            Debug.Log(request.isNetworkError
                ? $"[HighscoreManager-{uuid.ToString()}] Error: {request.error}"
                : $"[HighscoreManager-{uuid.ToString()}] Received: {request.downloadHandler.text}");

            callback(request.downloadHandler.text);
        }
    }


    [System.Serializable]
    private class SaveRequestBody
    {
        public Highscore score;

        public SaveRequestBody() {}
        public SaveRequestBody(String name, int scoreValue)
        {
            score = new Highscore(name, scoreValue);
        }
    }
}

