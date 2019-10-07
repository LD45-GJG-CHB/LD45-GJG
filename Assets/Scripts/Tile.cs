using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TextMeshProUGUI letter;

    public bool IsActivated => letter.color == GameState.currentTheme.FontColor;

    public TileType tileype = TileType.LETTER;

    public float a = .25f;
    public TMP_FontAsset _font; // for testing n shit

    public string fontBasePath = "Fonts & Materials";

    private BoxCollider2D _collider;
    public static readonly Dictionary<TextFont, string> FontPaths = new Dictionary<TextFont, string>
    {
        {TextFont.FiraCode, "FiraMono-Regular SDF"},
        {TextFont.Dotty, "dotty SDF"},
        {TextFont.Joystix, "joystix monospace SDF"},
        {TextFont.ComicSans, "COMIC SDF"}
    };

    private void Awake()
    {
        letter.color = GameState.currentTheme.FontColor;
        Camera.main.backgroundColor = GameState.currentTheme.BackgroundColor;
        if (FontPaths.TryGetValue(GameState.Font, out var path))
        {
            var font = Resources.Load<TMP_FontAsset>(Path.Combine(fontBasePath, path));
            letter.font = font;
        }
        else
        {
            Debug.LogError($"Font {GameState.Font} not defined in Tile.cs");
        }

//        letter.font = _font;
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
        letter.color = GameState.currentTheme.FontColor;
        _collider.enabled = true;
    }

    public void Deactivate()
    {
        if (!Player.Instance.letters.Contains(letter.text))
        {
            return;
        }

        var col = GameState.currentTheme.FontColor;
        col.a = a;
        letter.color = col;
        
        _collider.enabled = false;
    }
}