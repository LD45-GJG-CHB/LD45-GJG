using System.Linq;
using UnityEngine;

public class MenuPage : MonoBehaviour
{
    private MenuRoot _root;

    public MenuRoot Root => _root;

    private MenuEntry[] _menuEntries;
    private int _activeIndex;
    private MenuEntry Active => _menuEntries[_activeIndex];
    
    // Start is called before the first frame update
    void Start()
    {
        _menuEntries = gameObject.GetComponentsInChildren<MenuEntry>();

        foreach (var menuEntry in _menuEntries)
        {
            menuEntry.AttachPage(this);
        }
        SetActive(0);
    }

    // Update is called once per frame
    void Update()
    {
        HandleActions();
    }

    private void HandleActions()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (_activeIndex >= _menuEntries.Length - 1) return;
            SetActive(++_activeIndex);
        } else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (_activeIndex <= 0) return;
            SetActive(--_activeIndex);
        } else if (Input.GetKeyDown(KeyCode.Return))
        {
            Active.Select();
        }
    }
    
    public void setRoot(MenuRoot root)
    {
        _root = root;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }
    
    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetActive(MenuEntry menuEntry)
    {
        SetActive(_menuEntries.ToList().IndexOf(menuEntry));
    }

    public void SetActive(int index)
    {
        var i = 0;
        foreach (var menuEntry in _menuEntries)
        {
            if (i == index)
            {
                menuEntry.SetActive();
            }
            else
            {
                menuEntry.SetInactive();
            }
            i++;
        }
    }
}
