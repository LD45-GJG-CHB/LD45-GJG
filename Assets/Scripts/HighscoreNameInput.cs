using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighscoreNameInput : MonoBehaviour
{

    public TextMeshProUGUI nameInput;
    public String nextScene;
    public Score score;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void saveName()
    {
        var name = nameInput.text;
        Debug.Log($"[HighscoreNameInput] received name: {name.ToString()}");

        StartCoroutine(HighScoreAPI.Save(name, score.GetScore(), (s =>
            {
                SceneManager.LoadScene(nextScene, LoadSceneMode.Single);
            })));
        }
}
