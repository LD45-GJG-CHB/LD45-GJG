using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool DefaultVisible;
    public GameObject Target;
    public bool Active => Target.activeInHierarchy;
    
    void Start()
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

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        
        if (Active)
        {
            SetInvisible();
        }
        else
        {
            SetVisible();
        }
    }


    public void SetVisible()
    {
        Time.timeScale = 0f;
        Target.SetActive(true);
    }
    
    public void SetInvisible()
    {
        Time.timeScale = 1.0f;
        Target.SetActive(false);
    }
}
