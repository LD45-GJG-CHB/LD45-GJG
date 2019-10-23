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
    public List<string> mapNames;
    public int iterator = 0;
    public int initialScore = 500;
    public int scoreDecrementAmount = 10;
    public bool isCountingScore = true;
    public PauseMenu PauseMenu;
    public int waitTime = 6;
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
            if (GameState.isBonusMaps)
            {
                ScoreText.showText = false;
            }
        });
    }

    void Start()
    {
        SetFont();
        mapNames = MapLoader.Instance.mapNames;
        Debug.Log("Gamerunner Started");
        isCountingScore = false;
        ScoreText.showText = false;
        LevelChange();
        _levelText.text = "Tutorial";
        if (iterator == 1 && PlayerPrefs.HasKey("tutorial_finished") && PlayerPrefs.GetString("tutorial_finished") == "1" && !GameState.isBonusMaps) {
            _skipTutorial.text = "Press 9 to skip tutorial.";
        }
        if (!GameState.isBonusMaps)
        {
            StartCoroutine(DecrementScore());
        }
    }

    private void SetFont()
    {
         string fontBasePath = "Fonts & Materials";

         Dictionary<TextFont, string> FontPaths = new Dictionary<TextFont, string>
        {
            {TextFont.FiraCode, "FiraMono-Regular SDF"},
            {TextFont.Dotty, "dotty SDF"},
            {TextFont.Joystix, "joystix monospace SDF"},
            {TextFont.ComicSans, "COMIC SDF"}
        };
         
         if (FontPaths.TryGetValue(GameState.Font, out var path))
         {
             var font = Resources.Load<TMP_FontAsset>(Path.Combine(fontBasePath, path));

             foreach (var textMeshProUgui in (TextMeshProUGUI[]) FindObjectsOfTypeAll(typeof(TextMeshProUGUI)))
             {
                 textMeshProUgui.font = font;
             }
         }
         else
         {
             Debug.LogError($"Font {GameState.Font} not defined in Tile.cs");
         }
    }

    private void LevelChange()
    {
        WaitTimeCamera.Instance.SetCameraPriority(-1);
        MapRenderer.Instance.currentMap = mapNames[Instance.iterator++];
        Debug.Log(MapRenderer.Instance.currentMap);
        MapRenderer.Instance.DestroyTileMap();
        MapRenderer.Instance.LoadNextLevel();
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
        Debug.Log("Current Map: " + MapRenderer.Instance.currentMap);
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