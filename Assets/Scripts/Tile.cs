using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TextMeshProUGUI letter;
    public Color activatedColor = Color.white;
    public Color deactivatedColor = new Color(1,1,1, 0.5f);

    public bool IsActivated => letter.color == activatedColor;

    public TileType tileype = TileType.LETTER;
    
    private BoxCollider2D _collider;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();

        Activate();
        
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
        letter.color = deactivatedColor;
        _collider.enabled = false;
    }

}
