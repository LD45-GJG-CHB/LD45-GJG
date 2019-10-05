using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRunner : Singleton<GameRunner>
{

    public static List<string> mapNames = Maps.mapNames;
    public static int iterator = 1;
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
            return;
        }
        MapLoader.Instance.DestroyTileMap();
        MapLoader.Instance.currentMap = mapNames[iterator++];
        MapLoader.Instance.LoadNextLevel();
        Debug.Log("GameRunner: LoadNextLevel - Loaded!");
        Score.Instance.IncrementScore(Score.Instance.GetScore() + initialScore);
        isCountingScore = true;
    }

    void Start()
    {
        Debug.Log("Gamerunner Started");
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
