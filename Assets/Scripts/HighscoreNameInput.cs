using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreNameInput : MonoBehaviour
{

    public TMP_InputField InputField;
    public TextMeshProUGUI nameInput;
    public String nextScene;
    
    // Start is called before the first frame update
    void Start()
    {
        InputField.Select();
        InputField.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            saveName();
        }
    }

    public void saveName()
    {
        var score = GameState.score;
        GameState.score = 0;
        var name = nameInput.text;
        if (name == null || name == "")
        {
            name = "gamer";
        }
        GameState.playerName = name;
        
        Debug.Log($"[HighscoreNameInput] received name: {name}");
        Debug.Log($"[HighscoreNameInput] received score: {score}");

        StartCoroutine(HighScoreAPI.Save(name, score, s =>
        {
            var response = JsonUtility.FromJson<Response>(s);
            GameState.playerLastHighscore = response.data;
            SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
        }));
    }
    
    [System.Serializable]
    public class Response
    {
        public Highscore data;
    }
}
