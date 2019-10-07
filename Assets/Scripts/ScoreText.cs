using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    private TextMeshProUGUI _textField;
    public static bool showText = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _textField = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (showText)
        {
            _textField.text = $"Score: {Score.Instance.GetScore().ToString()}";
        }
    }
}
