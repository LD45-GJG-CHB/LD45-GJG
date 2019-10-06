using UnityEngine;

public class MenuPage : MonoBehaviour
{
    public MenuPageContent content;
    private MenuPage[] _menuPages;
    private MenuPage _active;
    private MenuPage _parent;
    private MenuPage _root;

    // Start is called before the first frame update
    void Start()
    {
        _menuPages = GetComponentsInChildren<MenuPage>();
        
        foreach (var menuPage in _menuPages)
        {
            menuPage.Disable();
            menuPage.setParent(this);
            menuPage.setRoot(_root ? _root : this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setParent(MenuPage parent)
    {
        _parent = parent;
    }
    
    public void setRoot(MenuPage root)
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
    
}
