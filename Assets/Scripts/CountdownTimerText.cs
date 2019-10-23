using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimerText : Singleton<CountdownTimerText>
{
    private TextMeshProUGUI textField;

    private void Awake()
    {
        textField = gameObject.GetComponent<TextMeshProUGUI>();
        textField.enabled = false;
    }

    void FixedUpdate()
    {
        textField.text = GetCountDownAsString();
    }

    public void SetEnabled(bool enabled)
    {
        textField.enabled = enabled;
    }

    private string GetCountDownAsString()
    {
        return ((int)GameRunner.Instance.countDown + 1).ToString();
    }
}
