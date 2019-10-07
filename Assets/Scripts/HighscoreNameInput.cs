using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        var diff = Enum.GetName(typeof(Difficulty), GameState.Difficulty);
        var score = GameState.score;
        GameState.score = 0;
        var gamertag = nameInput.text;

        if (string.IsNullOrWhiteSpace(gamertag) || gamertag.Length < 2)
        {
            gamertag = "gamer";
        }

        Debug.Log($"[HighscoreNameInput] received name: {gamertag}");
        Debug.Log($"[HighscoreNameInput] received score: {score}");

        StartCoroutine(HighScoreAPI.Save(gamertag, score, diff, s =>
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
