using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighScoreText : MonoBehaviour
{
    private TextMeshProUGUI _textField;


    // Start is called before the first frame update
    void Start()
    {
        _textField = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        var lastHighscore = GameState.PlayerLastHighscore;
        _textField.text = lastHighscore != null ? $"Well done, <i>{lastHighscore.name}</i>.                        You are #{lastHighscore.place.ToString()}" : $"";
    }
}
