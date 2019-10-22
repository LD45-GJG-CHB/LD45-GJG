using System;
using System.Collections.Generic;
using System.IO;
using Menus;
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
    
    public void SetFont(String theme)
    {
        var f = TextFont.FiraCode;
        switch (theme)
        {
            case "comicsans":
                f = TextFont.ComicSans;
                break;
            case "dotty":
                f = TextFont.Dotty;
                break;
            case "joystix":
                f = TextFont.Joystix;
                break;
            case "firacode":
            default:
                f = TextFont.FiraCode;
                break;
        }
        
        GameState.Font = f;
        
         var fontBasePath = "Fonts & Materials";

         var FontPaths = new Dictionary<TextFont, string>
        {
            {TextFont.FiraCode, "FiraMono-Regular SDF"},
            {TextFont.Dotty, "dotty SDF"},
            {TextFont.Joystix, "joystix monospace SDF"},
            {TextFont.ComicSans, "COMIC SDF"}
        };
         
         if (FontPaths.TryGetValue(GameState.Font, out var path))
         {
             var font = Resources.Load<TMP_FontAsset>(Path.Combine(fontBasePath, path));

             foreach (var textMeshProUgui in (TextMeshProUGUI[]) FindObjectsOfTypeAll(typeof(TextMeshProUGUI)))
             {
                 textMeshProUgui.font = font;
             }
         }
         else
         {
             Debug.LogError($"Font {GameState.Font} not defined in Tile.cs");
         }
         
         ThemeUpdater.Instance.UpdateTheme();


         
         

    }

    public void SetBonusMaps(bool bonusMaps)
    {
        GameState.isBonusMaps = bonusMaps;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
