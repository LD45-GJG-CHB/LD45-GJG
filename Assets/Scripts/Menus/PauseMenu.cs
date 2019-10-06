using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool DefaultVisible;
    public GameObject Target;
    
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
        Target.SetActive(true);
    }
    
    public void SetInvisible()
    {
        Time.timeScale = 1.0f;
        Target.SetActive(false);
    }
}
