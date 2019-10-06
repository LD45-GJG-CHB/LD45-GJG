using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuEntry : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI _textField;
    private bool _active;
    private MenuAction _menuAction;
    private MenuPage _page;

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

    public void AttachPage(MenuPage page)
    {
        _page = page;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetActive();
        _page.ClearIndex();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetInactive();
        _page.ClearIndex();
    }
}
