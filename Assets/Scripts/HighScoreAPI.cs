using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class HighScoreApi
{
    private static readonly string HIGHSCORES_API_URI = "http://167.99.142.75";

    public static IEnumerator GetTopList(Action<string> callback)
    {
        var uri = $"{HIGHSCORES_API_URI}/game/scores/top?limit=20";
        
        var uuid = Guid.NewGuid();
        Debug.Log($"[HighscoreManager-{uuid.ToString()}] GET {uri}");
        using (var request = UnityWebRequest.Get(uri))
        {
            yield return request.SendWebRequest();

            Debug.Log(request.isNetworkError
                ? $"[HighscoreManager-{uuid.ToString()}] Error: {request.error}"
                : $"[HighscoreManager-{uuid.ToString()}] Received: {request.downloadHandler.text}");

            callback(request.downloadHandler.text);
        }
    }

    public static IEnumerator Save(string name, int score, string difficulty, Action<string> callback)
    {
        var uri = $"{HIGHSCORES_API_URI}/game/scores";
        
        var uuid = Guid.NewGuid();
        Debug.Log($"[HighscoreManager-{uuid.ToString()}] POST {uri}");

        var body = new SaveRequestBody(name.Replace("\n", " "), score, difficulty);
        Debug.Log(JsonUtility.ToJson(body));
        using (var request = UnityWebRequest.Put(uri, JsonUtility.ToJson(body)))
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


    [Serializable]
    private class SaveRequestBody
    {
        public Highscore score;

        public SaveRequestBody() {}
        public SaveRequestBody(string name, int scoreValue, string difficulty)
        {
            score = new Highscore(name, scoreValue, difficulty);
        }
    }
}

