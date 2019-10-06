using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : Singleton<PauseMenu>
{
    private void OnEnable()
    {
//        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
//        Time.timeScale = 1.0f;
    }
}
