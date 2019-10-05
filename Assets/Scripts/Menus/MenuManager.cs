using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private MenuPage[] _menuPages;
    private MenuPage _active;

    // Start is called before the first frame update
    void Start()
    {
        _menuPages = GetComponentsInChildren<MenuPage>();
        
        foreach (var menuPage in _menuPages)
        {
            menuPage.Disable();
        }

        _active = _menuPages[0];
        _active.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Switch(MenuPage menuPage)
    {
        _active.Disable();
        _active = menuPage;
        _active.Enable();
    }
}
