using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameRunner : Singleton<GameRunner>
{
    public int initialScore = 500;
    public bool isCountingScore = true;
    public int waitTime = 3;
    public float countDown;
    private bool tutorialSkipped = false;

    public PauseMenu PauseMenu;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI skipTutorial;
    private IEnumerator levelWaitStartTime;

    // Loads first level
    void Start()
    {
        levelText.text = "Tutorial";
        isCountingScore = false;
        LevelChange();
        if (IsTutorialFinished())
        {
            skipTutorial.enabled = true;
            skipTutorial.text = "Press 9 to skip tutorial.";
        }
        else
        {
            skipTutorial.enabled = false;
        }

        if (!GameState.isBonusMaps)
        {
            StartCoroutine(DecrementScore());
        }
    }

    private bool IsTutorialFinished()
    {
        return MapRenderer.Instance.GetMapIndex() == 1
            && PlayerPrefs.HasKey("tutorial_finished")
            && PlayerPrefs.GetString("tutorial_finished") == "1"
            && !GameState.isBonusMaps;
    }

    public void LoadNextLevel()
    {
        skipTutorial.enabled = false;
        CountdownTimerText.Instance.SetEnabled(true);
        ScoreText.Instance.SetEnabled(!GameState.isBonusMaps);
        isCountingScore = false;

        if (levelWaitStartTime != null)
        {
            StopCoroutine(levelWaitStartTime);
        }

        if (MapRenderer.Instance.IsLastLevel())
        {
            DOTween.Sequence()
                .SetUpdate(true)
                .AppendCallback((() => Time.timeScale = 0.0f))
                .Append(Player.Instance._darkness.DOFade(1.0f, 0.6f))
                .AppendInterval(1.0f)
                .AppendCallback((() => Time.timeScale = 1.0f))
                .AppendCallback(() => SceneManager.LoadScene("HighscoreInputScene"));
            return;
        }

        levelWaitStartTime = LevelStartWaitTime();

        DOTweenSequnceBetweenLevels(() =>
        {
            Player.Instance.Velocity.x = Player.Instance._moveSpeed;
            levelText.text = $"Level {MapRenderer.Instance.GetMapIndex()}";
            LevelChange();
            isCountingScore = true;
            countDown = waitTime;
            Score.Instance.IncrementScore(initialScore);
            StartCoroutine(levelWaitStartTime);
        });
    }

    private void LevelChange()
    {
        WaitTimeCamera.Instance.SetCameraPriority(-1);
        MapRenderer.Instance.currentMap = MapLoader.Instance.mapNames[MapRenderer.Instance.GetMapIndex()];
        Debug.Log(MapRenderer.Instance.currentMap);
        MapRenderer.Instance.DestroyTileMap();
        MapRenderer.Instance.LoadNextLevel();

        if (GameState.Difficulty != Difficulty.PENULTIMATE_MAMBO_JAMBO && MapRenderer.Instance.GetMapIndex() == (MapLoader.Instance.mapNames.Count / 2) + 2)
        {
            Instance.StartCoroutine(AudioManager.Instance.FadeOut(2.5f));
//            AudioManager.Instance.StopAllMusic();
            AudioManager.Instance.Play("rEX", isLooping:true);
        } 
    }

    private static void PauseActions()
    {
        WaitTimeCamera.Instance.SetCameraPriority(110);
        Player.Instance.isWaiting = true;
        Instance.isCountingScore = false;
        Instance.countDown = Instance.waitTime;
    }

    private static void UnpauseActions()
    {
        WaitTimeCamera.Instance.SetCameraPriority(-1);
        Player.Instance.isWaiting = false;
        Instance.isCountingScore = true;
    }

    private static IEnumerator LevelStartWaitTime()
    {
        PauseActions();
        yield return new WaitForSeconds(Instance.waitTime);
        UnpauseActions();
    }

    private static IEnumerator DecrementScore(int decrement = 10)
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Instance.isCountingScore)
            {
                Score.Instance.DecrementScore(decrement);
            }
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            LoadNextLevel();
        }
        if (countDown >= 0)
        {
            countDown -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && !tutorialSkipped)
        {
            tutorialSkipped = true;
            if (IsTutorialFinished())
            {
                LoadNextLevel();
            }
        }
    }

    public void DOTweenSequnceBetweenLevels(TweenCallback callBackFunction)
    {
        DOTween.Sequence()
            .SetUpdate(true)
            .AppendCallback((() => Time.timeScale = 0.0f))
            .Append(Player.Instance._darkness.DOFade(1.0f, 0.2f))
            .AppendInterval(0.1f)
            .AppendCallback(callBackFunction)
            .AppendCallback((() => Time.timeScale = 1.0f))
            .AppendInterval(0.2f)
            .Append(Player.Instance._darkness.DOFade(0.0f, 0.4f))
            .AppendInterval(0.2f)
            .Play();
    }

    public void PlayerOutOfBoundsReset()
    {
        isCountingScore = false;
        DOTweenSequnceBetweenLevels(() =>
        {
            Player.Instance.transform.position = new Vector3(MapRenderer.Instance.playerStartPosX, MapRenderer.Instance.playerStartPosY, 0);
            Player.Instance.Velocity = Vector3.zero;
            Player.Instance.Velocity.x = Player.Instance._moveSpeed;
            Score.Instance.DecrementScore(25);
            countDown = waitTime;
            isCountingScore = true;
            StartCoroutine(LevelStartWaitTime());
        });
    }

}