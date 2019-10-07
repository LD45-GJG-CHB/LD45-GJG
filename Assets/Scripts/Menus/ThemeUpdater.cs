using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ThemeUpdater : Singleton<ThemeUpdater>
{
    public TextMeshProUGUI TextComponent;
    public Material PanelMaterial;
    public Material PlayerMaterial;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake()
    {
        UpdateTheme();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdatePanelMaterialColor()
    {
    }
    

    public void UpdateTextColor()
    {
    }
    
    public void UpdateTheme()
    {
        // Update player material
        PlayerMaterial.SetColor("_Color", ThemeManager.GetCurrentTheme().FontColor);
        // Update panel material
        PanelMaterial.SetColor("_Color", ThemeManager.GetCurrentTheme().BackgroundColor);
        // Update text color
        TextComponent.fontSharedMaterial.SetColor(ShaderUtilities.ID_FaceColor, ThemeManager.GetCurrentTheme().FontColor);
    }
}
