using System;
using UnityEngine;

public class CameraEffectManager : MonoBehaviour
{
    public Shader trippyShader;

    private Material _trippyMat;


    private void Awake()
    {
        _trippyMat = new Material(trippyShader){hideFlags = HideFlags.HideAndDontSave};
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        var rt = RenderTexture.GetTemporary(src.width, src.height, 0, src.format);

        Graphics.Blit(src, dest, _trippyMat);
        
        RenderTexture.ReleaseTemporary(rt);
    }
}