using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TextMeshProUGUI letter;
    public Color activatedColor = Color.white;
    public Color deactivatedColor = new Color(1, 1, 1, 0.5f);

    public bool IsActivated => letter.color == activatedColor;

    public TileType tileype = TileType.LETTER;

    private BoxCollider2D _collider;

    public TMP_FontAsset _font; // for testing n shit

    public string fontBasePath = "Fonts & Materials";

    public static readonly Dictionary<TextFont, string> FontPaths = new Dictionary<TextFont, string>
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
        letter.color = activatedColor;
        _collider.enabled = true;
    }

    public void Deactivate()
    {
        if (!Player.Instance.letters.Contains(letter.text))
        {
            return;
        }

        letter.color = deactivatedColor;
        _collider.enabled = false;
    }
}