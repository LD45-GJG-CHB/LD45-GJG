using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownTimerText : MonoBehaviour
{
    private TextMeshProUGUI _textField;

    private bool _countDownEnded;
    // Start is called before the first frame update
    void Start()
    {
        _textField = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameRunner.Instance.countDown <= 0 || MapLoader.Instance.currentMap == Maps.Instance.tutorialMap )
        {
//            return;
            _textField.text = "";
        } else
        {
            _textField.text = ((int)GameRunner.Instance.countDown + 1).ToString();
        }
    }
}
