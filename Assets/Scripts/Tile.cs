using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TextMeshProUGUI letter;
    public Color activatedColor = Color.white;
    public Color deactivatedColor = new Color(1,1,1, 0.6f);

    public bool IsActivated => letter.color == activatedColor;

    public TileType tileype = TileType.LETTER;
    
    private BoxCollider2D _collider;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        letter.color = deactivatedColor;
//        _collider.enabled = false;
        
    }

    public void SetLetter(string textLetter)
    {
        letter.text = textLetter;
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
