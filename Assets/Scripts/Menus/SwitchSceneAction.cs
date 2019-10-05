using System;
using UnityEngine.SceneManagement;

public class SwitchSceneAction : MenuAction
{
    public String switchTo;
    
    public override void doAction()
    {
        SceneManager.LoadScene(switchTo, LoadSceneMode.Single);
    }
}
