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
    public Score score;
    
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
        var name = nameInput.text;
        GameState.playerName = name;
        
        Debug.Log($"[HighscoreNameInput] received name: {name}");

        StartCoroutine(HighScoreAPI.Save(name, score.GetScore(), s =>
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
