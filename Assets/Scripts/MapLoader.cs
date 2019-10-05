using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : Singleton<MapLoader>
{
    public string currentMap = "map_0"; // Somehow edit this variable between levels
    public string currentFont = "font_0"; // Somehow edit this variable between levels

    public GameObject tilePrefab;
    public GameObject exitPrefab;
    public GameObject edgeTilePrefab;
    public float tileSize = 2;

    private string[,] map;

    private void Awake()
    {
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        map = Maps.StringTo2DArray(Maps.maps[currentMap]);

        for (var y = 0; y < Maps.mapSizeY; y++)
        {
            for (var x = 0; x < Maps.mapSizeX; x++)
            {
                var letter = map[x, y];

                switch (letter)
                {
                    case "1":
                    {
                        var posX = x * tileSize;
                        var posY = y * -tileSize;

                        Player.Instance.transform.parent.position = new Vector3(posX, posY, 0);
                        break;
                    }

                    case "2":
                    {
                        var tile = Instantiate(exitPrefab, transform);
                        var posX = x * tileSize;
                        var posY = y * -tileSize;

                        tile.transform.position = new Vector2(posX, posY);

                        break;
                    }

                    case "3":
                    {
                        var tile = Instantiate(edgeTilePrefab, transform);
                        var posX = x * tileSize;
                        var posY = y * -tileSize;

                        tile.transform.position = new Vector2(posX, posY);

                        break;
                    }

                    case "-":
                        break;
                    default:
                    {
                        var tile = Instantiate(tilePrefab, transform);

                        var posX = x * tileSize;
                        var posY = y * -tileSize;

                        tile.transform.position = new Vector2(posX, posY);

                        tile.GetComponent<Tile>().SetLetter(letter);
                        break;
                    }
                }
            }
        }
    }
}