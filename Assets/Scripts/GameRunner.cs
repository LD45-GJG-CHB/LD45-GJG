using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRunner : Singleton<GameRunner>
{
    public List<string> mapNames;
    public int iterator = 0;
    public int initialScore = 500;
    public int scoreDecrementAmount = 10;
    public bool isCountingScore = true;
    public PauseMenu PauseMenu;
    public int waitTime = 3;
    public float countDown;
    private bool tutorialSkipped = false;

    public TextMeshProUGUI _levelText;
    public TextMeshProUGUI _skipTutorial;

    private IEnumerator levelWaitStartTime;

    public void LoadNextLevel()
    {
        if (levelWaitStartTime != null)
        {
            StopCoroutine(levelWaitStartTime);
        }
        _skipTutorial.text = "";
        isCountingScore = false;
        if (iterator == mapNames.Count)
        {
            GameState.score = Score.Instance.GetScore();
            Debug.Log("The End!");
            DOTween.Sequence()
                .SetUpdate(true)
                .AppendCallback((() => Time.timeScale = 0.0f))
                .Append(Player.Instance._darkness.DOFade(1.0f, 0.6f))
                .AppendInterval(1.0f)
                .AppendCallback((() => Time.timeScale = 1.0f))
                .AppendCallback(() => SceneManager.LoadScene("HighscoreInputScene"));
            return;
        }

        Debug.Log("GameRunner: LoadNextLevel...");
        levelWaitStartTime = LevelStartWaitTime();
        DOTweenSequnceBetweenLevels(() =>
        {
            Player.Instance.Velocity.x = Player.Instance._moveSpeed;
            ScoreText.showText = true;
            _levelText.text = $"Level {iterator}";
            LevelChange();
            isCountingScore = true;
            countDown = waitTime;
            Score.Instance.IncrementScore(initialScore);
            StartCoroutine(levelWaitStartTime);
        });
    }

    void Start()
    {
        mapNames = Maps.Instance.mapNames;
        Debug.Log("Gamerunner Started");
        isCountingScore = false;
        ScoreText.showText = false;
        LevelChange();
        _levelText.text = "Tutorial";
        if (iterator == 1 && PlayerPrefs.HasKey("tutorial_finished") && PlayerPrefs.GetString("tutorial_finished") == "1") {
            _skipTutorial.text = "Press 9 to skip tutorial.";
        }
        StartCoroutine(DecrementScore());
    }

    private void LevelChange()
    {
        WaitTimeCamera.Instance.SetCameraPriority(-1);
        MapLoader.Instance.currentMap = mapNames[Instance.iterator++];
        MapLoader.Instance.DestroyTileMap();
        MapLoader.Instance.LoadNextLevel();
        LogLevelLoaded();

        if (GameState.Difficulty != Difficulty.PENULTIMATE_MAMBO_JAMBO && Instance.iterator == (mapNames.Count / 2) + 2)
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

    private static void LogLevelLoaded()
    {
        Debug.Log("Current Map: " + MapLoader.Instance.currentMap);
        Debug.Log("GameRunner: LoadNextLevel - Loaded!");
    }

    private void Update()
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
            if (iterator == 1 && PlayerPrefs.HasKey("tutorial_finished") && PlayerPrefs.GetString("tutorial_finished") == "1")
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
            Player.Instance.transform.position = new Vector3(MapLoader.Instance.playerStartPosX, MapLoader.Instance.playerStartPosY, 0);
            Player.Instance.Velocity = Vector3.zero;
            Player.Instance.Velocity.x = Player.Instance._moveSpeed;
            Score.Instance.DecrementScore(25);
            countDown = waitTime;
            isCountingScore = true;
            StartCoroutine(LevelStartWaitTime());
        });
    }

}