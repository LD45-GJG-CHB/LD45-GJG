using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimerText : Singleton<CountdownTimerText>
{
    private TextMeshProUGUI _textField;

    private void Awake()
    {
        _textField = gameObject.GetComponent<TextMeshProUGUI>();
        _textField.enabled = false;
    }

    void FixedUpdate()
    {
        _textField.text = GameRunner.Instance.GetCountDownAsString();
    }

    public void SetEnabled(bool enabled)
    {
        _textField.enabled = enabled;
    }
    
}
