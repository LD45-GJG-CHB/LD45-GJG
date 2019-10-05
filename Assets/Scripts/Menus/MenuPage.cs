using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPage : MonoBehaviour
{
    private MenuEntry[] _menuEntries;
    private int _activeIndex;

    private MenuEntry Active => _menuEntries[_activeIndex];

    // Start is called before the first frame update
    void Start()
    {
        _menuEntries = GetComponentsInChildren<MenuEntry>();
        _activeIndex = 0;
        Active.SetActive();
    }

    // Update is called once per frame
    void Update()
    {
        if (ApplicationSettings.IsPaused()) return;
        
        HandleActions();   
    }

    void HandleActions()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            IncrementIndex();
        } else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            DecrementIndex();
        } else if (Input.GetKeyDown(KeyCode.Return))
        {
            Active.Select();
        }
        
    }

    void IncrementIndex()
    {
        if (_activeIndex >= _menuEntries.Length - 1) return;
        
        Active.SetInactive();
        _activeIndex += 1;
        Active.SetActive();
    }
    
    void DecrementIndex()
    {
        if (_activeIndex <= 0) return;
        
        Active.SetInactive();
        _activeIndex -= 1;
        Active.SetActive();
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
    
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
