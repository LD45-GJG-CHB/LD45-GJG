using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRunner : Singleton<GameRunner>
{

    public static List<string> mapNames = Maps.mapNames;
    public static int iterator = 0;
    public static int initialScore = 500;
    public static int scoreDecrementAmount = 10;
    public static bool isCountingScore = true;

    public static void LoadNextLevel()
    {
        Debug.Log("GameRunner: LoadNextLevel...");
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

        DOTween.Sequence()
            .SetUpdate(true)
            .AppendCallback((() => Time.timeScale = 0.0f))
            .Append(Player.Instance._darkness.DOFade(1.0f, 0.2f))
            .AppendInterval(0.1f)
            .AppendCallback(() =>
            {
                MapLoader.Instance.currentMap = mapNames[iterator++];
                MapLoader.Instance.DestroyTileMap();
                MapLoader.Instance.LoadNextLevel();
                Debug.Log("GameRunner: LoadNextLevel - Loaded!");
                Score.Instance.IncrementScore(initialScore);
                isCountingScore = true;
                Player.Instance.Velocity = Vector3.zero;
                Player.Instance.Velocity.x = Player.Instance._moveSpeed;
            })
            .Append(Player.Instance._darkness.DOFade(0.0f, 0.3f))
            .AppendInterval(0.2f)
            .AppendCallback((() => Time.timeScale = 1.0f))
            .Play();
    }

    void Start()
    {
        Debug.Log("Gamerunner Started");
        MapLoader.Instance.currentMap = mapNames[iterator++];
        MapLoader.Instance.DestroyTileMap();
        MapLoader.Instance.LoadNextLevel();
        Score.Instance.IncrementScore(initialScore);
        StartCoroutine(DecrementScore());
    }

    IEnumerator DecrementScore(int decrement = 10)
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (isCountingScore)
            {
                Score.Instance.DecrementScore(decrement);
            }
        }
    }

}
