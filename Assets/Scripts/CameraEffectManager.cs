using System;
using UnityEngine;

public class CameraEffectManager : MonoBehaviour
{
    public Shader trippyShader;

    public Material trippyMat;


    private void Awake()
    {
        if (GameState.Difficulty == Difficulty.PENULTIMATE_MAMBO_JAMBO)
        {
            AudioManager.Instance.Play("Destructor", .35f, true);
        }
        else
        {
            AudioManager.Instance.Play("Frontendd", .75f, true);
        }
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        var rt = RenderTexture.GetTemporary(src.width, src.height, 0, src.format);
 
        if (GameState.Difficulty == Difficulty.PENULTIMATE_MAMBO_JAMBO)
        {
            Graphics.Blit(src, dest, trippyMat);
        }
        else
        {
            Graphics.Blit(src, dest);
        }
        
        RenderTexture.ReleaseTemporary(rt);
    }
}