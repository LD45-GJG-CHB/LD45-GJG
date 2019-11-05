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
    private const int InitialScore = 500;
    private const int WaitTime = 3;

    private bool _isCountingScore;
    private bool _tutorialSkipped;
    private float _countDownTime;

    private IEnumerator _levelWaitStartTime;
    
    public PauseMenu PauseMenu;
    public TextMeshProUGUI levelText; //text value is set in unity
    public TextMeshProUGUI skipTutorial; //text value is set in unity

    // Loads first level
    private void Start()
    {
        Camera.main.backgroundColor = GameState.CurrentTheme.backgroundColor;
        skipTutorial.enabled = IsTutorialFinished();
        LoadMap(GetNextMapName());
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            LoadNextLevel();
        }
        if (_countDownTime >= 0)
        {
            _countDownTime -= Time.deltaTime;
        }
        if (!_tutorialSkipped && Input.GetKeyDown(KeyCode.Alpha9) && IsTutorialFinished())
        {
            _tutorialSkipped = true;
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        skipTutorial.enabled = false;
        CountdownTimerText.Instance.SetEnabled(false);
        ScoreText.Instance.SetEnabled(!GameState.isBonusMaps);
        _isCountingScore = false;
        
        if (!GameState.isBonusMaps)
        {
            StartCoroutine(DecrementScore());
        }
        
        if (_levelWaitStartTime != null)
        {
            StopCoroutine(_levelWaitStartTime);
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

        _levelWaitStartTime = LevelStartWaitTime();

        DOTweenSequnceBetweenLevels(() =>
        {
            Player.Instance.Velocity.x = Player.Instance._moveSpeed;
            CountdownTimerText.Instance.SetEnabled(true);
            levelText.text = $"Level {MapRenderer.Instance.GetMapIndex()}";
            LoadMap(GetNextMapName());
            _isCountingScore = true;
            _countDownTime = WaitTime;
            Score.Instance.IncrementScore(InitialScore);
            StartCoroutine(_levelWaitStartTime);
        });
    }

    private string GetNextMapName()
    {
        return MapLoader.Instance.mapNames[MapRenderer.Instance.GetMapIndex()];
    }

    private void LoadMap(string mapName)
    {
        WaitTimeCamera.Instance.SetCameraPriority(-1);
        MapRenderer.Instance.DestroyTileMap();
        MapRenderer.Instance.LoadNextLevel(mapName);

        if (GameState.CurrentDifficulty.difficultyLevel != DifficultyLevel.PENULTIMATE_MAMBO_JAMBO && MapRenderer.Instance.GetMapIndex() == (MapLoader.Instance.mapNames.Count / 2) + 2)
        {
            Instance.StartCoroutine(AudioManager.Instance.FadeOut(2.5f));
            AudioManager.Instance.Play("rEX", isLooping:true);
        } 
    }

    private void PauseActions()
    {
        WaitTimeCamera.Instance.SetCameraPriority(110);
        Player.Instance.isWaiting = true;
        _isCountingScore = false;
        _countDownTime = WaitTime;
    }

    private void UnpauseActions()
    {
        WaitTimeCamera.Instance.SetCameraPriority(-1);
        Player.Instance.isWaiting = false;
        _isCountingScore = true;
        CountdownTimerText.Instance.SetEnabled(false);
    }

    private IEnumerator LevelStartWaitTime()
    {
        PauseActions();
        yield return new WaitForSeconds(WaitTime);
        UnpauseActions();
    }

    private IEnumerator DecrementScore(int decrement = 10)
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (_isCountingScore)
            {
                Score.Instance.DecrementScore(decrement);
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
        _isCountingScore = false;
        DOTweenSequnceBetweenLevels(() =>
        {
            Player.Instance.transform.position = new Vector3(MapRenderer.Instance.playerStartPosX, MapRenderer.Instance.playerStartPosY, 0);
            Player.Instance.Velocity = Vector3.zero;
            Player.Instance.Velocity.x = Player.Instance._moveSpeed;
            Score.Instance.DecrementScore(25);
            CountdownTimerText.Instance.SetEnabled(true);
            _countDownTime = WaitTime;
            _isCountingScore = true;
            StartCoroutine(LevelStartWaitTime());
        });
    }
    
    private bool IsTutorialFinished()
    {
        return !GameState.isBonusMaps
               && MapRenderer.Instance.IsTutorial()
               && PlayerPrefs.HasKey("tutorial_finished")
               && PlayerPrefs.GetString("tutorial_finished") == "1";
    }
    
    public string GetCountDownAsString()
    {
        return ((int) _countDownTime + 1).ToString();
    }

}