using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuEntry : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI _textComponent;
    private Button _buttonComponent;
    private bool _active;
    private MenuAction _menuAction;
    private MenuPage _menuPage;

    public MenuPage MenuPage => _menuPage;

    // Start is called before the first frame update
    void Start()
    {
        _textComponent = gameObject.GetComponent<TextMeshProUGUI>();
        _buttonComponent = gameObject.GetComponent<Button>();
        _menuAction = gameObject.GetComponent<MenuAction>();
    }

    // Update is called once per frame
    void Update()
    {
        _textComponent.fontStyle = _active ? FontStyles.Bold | FontStyles.Underline : FontStyles.Normal;
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

        _buttonComponent.onClick?.Invoke();
    }

    public void AttachPage(MenuPage menuPage)
    {
        _menuPage = menuPage;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetActive();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetInactive();
    }

    public void SwitchPage(MenuPage menuPage)
    {
        MenuPage.Root.Show(menuPage);
    }

    public void SwitchScene(String scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    public void SetTheme(String theme)
    {
        ThemeManager.SetCurrentTheme(theme);
        ThemeUpdater.Instance.UpdateTheme();
    }
}
