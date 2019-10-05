using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRunner : Singleton<GameRunner>
{

    public static List<string> mapNames = Maps.mapNames;
    public static int iterator = 1;

    public static void LoadNextLevel()
    {
        if (iterator == mapNames.Count)
        {
            Debug.Log("The End!");
            return;
        }
        MapLoader.Instance.DestroyTileMap();
        MapLoader.Instance.currentMap = mapNames[iterator++];
        MapLoader.Instance.LoadNextLevel();
    }
 
}
