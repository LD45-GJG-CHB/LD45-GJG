using System;
using TMPro;
using UnityEngine;

public class MenuEntry : MonoBehaviour
{
    private TextMeshProUGUI _textField;
    private bool _active;
    private MenuAction _menuAction;

    // Start is called before the first frame update
    void Start()
    {
        _textField = gameObject.GetComponent<TextMeshProUGUI>();
        _menuAction = gameObject.GetComponent<MenuAction>();
    }

    // Update is called once per frame
    void Update()
    {
        _textField.fontStyle = _active ? FontStyles.Bold : FontStyles.Normal;
    }

    public void SetActive()
    {
        _active = true;
    }

    public void SetInactive()
    {
        _active = false;
    }

    public void Select()
    {
        if (_menuAction)
        {
            _menuAction.doAction();
        }
    }
}
