using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPage : MonoBehaviour
{
    private MenuEntry[] _menuEntries;
    private int? _activeIndex;

    private MenuEntry Active => (_activeIndex != null) ? _menuEntries[_activeIndex.Value] : null;

    // Start is called before the first frame update
    void Start()
    {
        _menuEntries = GetComponentsInChildren<MenuEntry>();
        foreach (var menuEntry in _menuEntries)
        {
            menuEntry.AttachPage(this);
        }
        _activeIndex = 0;
        Active.SetActive();
    }

    // Update is called once per frame
    void Update()
    {
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

    public void ClearIndex()
    {
        _activeIndex = null;
    }

    void IncrementIndex()
    {
        if (_activeIndex == null)
        {
            _activeIndex = 0;
        }
        if (_activeIndex >= _menuEntries.Length - 1) return;
        
        Active.SetInactive();
        _activeIndex += 1;
        Active.SetActive();
    }
    
    void DecrementIndex()
    {
        if (_activeIndex == null)
        {
            _activeIndex = 0;
        }
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
