using UnityEngine;

public class MenuDisplayer : MonoBehaviour
{
    public MenuManager MenuManager;
    public bool DefaultVisible;
    
    private void Start()
    {
        if (DefaultVisible)
        {
            SetVisible();
        }
        else
        {
            SetInvisible();
        }
    }

    public void SetVisible()
    {
        Time.timeScale = 0f;
        MenuManager.gameObject.SetActive(true);
    }
    
    public void SetInvisible()
    {
        Time.timeScale = 1.0f;
        MenuManager.gameObject.SetActive(false);   
    }
}
