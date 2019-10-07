using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimerText : MonoBehaviour
{
    private TextMeshProUGUI _textField;

    // Start is called before the first frame update
    void Start()
    {
        _textField = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameRunner.Instance.countDown < 0)
        {
            _textField.text = "";
        } else
        {
            _textField.text = ((int)GameRunner.Instance.countDown + 1).ToString();
        }
    }
}
