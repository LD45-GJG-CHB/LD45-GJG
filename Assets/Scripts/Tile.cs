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

    private void Awake()
    {
        var font = ThemeManager.Instance.GetFont(GameState.Font);

        letter.font = font;
        letter.color = GameState.CurrentTheme.fontColor;
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