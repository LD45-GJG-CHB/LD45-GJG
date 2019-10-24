using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Themes;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TextMeshPro letter;

    public bool IsActivated => letter.color == GameState.CurrentTheme.fontColor;

    public TileType tileype = TileType.LETTER;
    
    private BoxCollider2D _collider;
    public string fontBasePath = "Fonts & Materials";

    private static readonly Dictionary<TextFont, string> FontPaths = new Dictionary<TextFont, string>
    {
        {TextFont.FiraCode, "FiraMono-Regular SDF"},
        {TextFont.Dotty, "dotty SDF"},
        {TextFont.Joystix, "joystix monospace SDF"},
        {TextFont.ComicSans, "COMIC SDF"}
    };

    private void Awake()
    {
        if (FontPaths.TryGetValue(GameState.Font, out var path))
        {
            var font = Resources.Load<TMP_FontAsset>(Path.Combine(fontBasePath, path));
            letter.font = font;
        }
        else
        {
            Debug.LogError($"Font {GameState.Font} not defined in Tile.cs");
        }
        letter.color = GameState.CurrentTheme.fontColor;
        Camera.main.backgroundColor = GameState.CurrentTheme.backgroundColor;
    }

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();

        Deactivate();
    }

    public void SetLetter(string textLetter)
    {
        letter.text = textLetter;
    }

    public void ToggleState()
    {
        if (IsActivated)
        {  
            Deactivate();
        }
        else
        {
            Activate();
        }
    }


    public void Activate()
    {
        letter.color = GameState.CurrentTheme.fontColor;
        _collider.enabled = true;
    }

    public void Deactivate()
    {
        if (!Player.Instance.letters.Contains(letter.text))
        {
            return;
        }

        var col = GameState.CurrentTheme.fontColor;
        col.a = GameState.CurrentTheme.inactiveFontAlpha;
        letter.color = col;
        
        _collider.enabled = false;
    }
}