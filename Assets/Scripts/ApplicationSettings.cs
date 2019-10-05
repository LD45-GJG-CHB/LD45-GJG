using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///     A general class to set various settings related to the application
/// </summary>
public class ApplicationSettings : MonoBehaviour
{
    private void Awake()
    {
        QualitySettings.vSyncCount = 0; // TODO : Make it possible to change vSync settings
        Application.targetFrameRate = (int) FramerateTarget.MAX_144; // TODO : Make it so you can set this in settings
    }

    private void Update() // hidden options
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            var enumValues = (int[]) Enum.GetValues(typeof(FramerateTarget));

            var targetFrameRate = Application.targetFrameRate;
            var currentFrameRate = Array.IndexOf(enumValues, targetFrameRate) + 1;

            Application.targetFrameRate =
                enumValues.Length == currentFrameRate ? enumValues[0] : enumValues[currentFrameRate];
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public static bool IsPaused() => Time.timeScale < float.Epsilon;
}

public enum FramerateTarget
{
    MAX_30 = 30,
    MAX_60 = 60,
    MAX_120 = 120,
    MAX_144 = 144,
    UNLIMITED = 1000 // not really unlimited but basically
}