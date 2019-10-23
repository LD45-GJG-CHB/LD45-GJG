using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : Singleton<ScoreText>
{
    private TextMeshProUGUI textField;

    private void Awake()
    {
        textField = gameObject.GetComponent<TextMeshProUGUI>();
        textField.enabled = false;
    }

    void FixedUpdate()
    {
        textField.text = GetScoreAsString();
    }

    public void SetEnabled(bool enabled)
    {
        textField.enabled = enabled;
    }

    private string GetScoreAsString()
    {
        return $"Score: {Score.Instance.GetScore().ToString()}";
    }
}
