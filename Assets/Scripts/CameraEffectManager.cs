using System;
using UnityEngine;

public class CameraEffectManager : MonoBehaviour
{
    public Shader trippyShader;

    public Material trippyMat;

    

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        var rt = RenderTexture.GetTemporary(src.width, src.height, 0, src.format);

        Graphics.Blit(src, dest, trippyMat);
        
        RenderTexture.ReleaseTemporary(rt);
    }
}