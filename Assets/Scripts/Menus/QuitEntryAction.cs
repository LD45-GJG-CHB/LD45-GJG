using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitEntryAction : MenuAction
{
    public override void doAction()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
