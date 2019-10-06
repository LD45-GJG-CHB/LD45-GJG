using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePauseMenuAction : MenuAction
{
    public override void doAction()
    {
        PauseMenu.Instance.gameObject.SetActive(false);
    }
}
