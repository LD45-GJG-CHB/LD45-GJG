using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Image))]
public class Panel : MonoBehaviour
{
    // Update is called once per frame
    private void Start()
    {
        UpdateColor();
    }

    void Update()
    {
//        UpdateColor();
    }

    void UpdateColor()
    {
        gameObject.GetComponent<Image>().color = ThemeManager.GetCurrentTheme().BackgroundColor;
    }
}
