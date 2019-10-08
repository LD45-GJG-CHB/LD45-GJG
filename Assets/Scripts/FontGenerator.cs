using System;
using System.IO;
using TMPro;
using UnityEngine;

public class FontGenerator : MonoBehaviour
{
    private void Awake()
    {
        Texture2D output = new Texture2D(200, 200);
        RenderTexture renderTexture = new RenderTexture(200, 200, 24);
        RenderTexture.active = renderTexture;
        GameObject tempObject = new GameObject("Temporary");
        Camera myCamera = tempObject.AddComponent<Camera>();
        myCamera.orthographic = true;
        myCamera.orthographicSize = 100;
        myCamera.targetTexture = renderTexture;
        Canvas canvas = tempObject.AddComponent<Canvas>();


        var childObject = new GameObject("Text");

        var text = childObject.AddComponent<TextMeshProUGUI>();

        childObject.transform.SetParent(tempObject.transform);

        text.text = "Hello\nWorld!";
//        guiText.anchor = TextAnchor.LowerLeft;
//        guiText.alignment = TextAlignment.Left;

        text.lineSpacing = 1;
//        guiText.pixelOffset = new Vector2(50,50);
        text.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/FiraMono-Regular SDF");
        myCamera.Render();
        output.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        output.Apply();

        File.WriteAllBytes(@"Assets\FontRendering\target\test.png", output.EncodeToPNG());
    }
}