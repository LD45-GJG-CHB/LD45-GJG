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
    public Dictionary<string, List<Tile>> tileMap;

    
    private string[,] map;

    private void Awake()
    {
        tileMap = new Dictionary<string, List<Tile>>();
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

                Tile tile;
                switch (letter)
                {
                    case "1": // player pos
                        var posX = x * tileSize;
                        var posY = y * -tileSize;

                        Player.Instance.transform.parent.position = new Vector3(posX, posY, 0);
                        continue;
                    case "-": // nothingness
                        continue;

                    case "@": // exit / door
                        tile = CreateTileAtPosition(x, y);

                        tile.GetComponent<Tile>().tileype = TileType.DOOR;

                        tile.GetComponent<BoxCollider2D>().isTrigger = true;

                        tile.gameObject.layer = 11;
                        Rigidbody2D rb = tile.gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
                        rb.gravityScale = 0.0f;
                        rb.isKinematic = true; 
                        tile.GetComponent<Tile>().SetLetter(letter);


                        break;
                    default: // default letter tile
                        tile = CreateTileAtPosition(x, y);

                        tile.GetComponent<Tile>().SetLetter(letter);

                        break;
                }

                if (tileMap.TryGetValue(letter, out var tiles))
                {
                    tiles.Add(tile);
                }
                else
                {
                    tileMap[letter] = new List<Tile> {tile};
                }

            }
        }
    }


    private Tile CreateTileAtPosition(int x, int y)
    {
        var tile = Instantiate(tilePrefab, transform);
        var posX = x * tileSize;
        var posY = y * -tileSize;

        tile.transform.position = new Vector2(posX, posY);

        return tile.GetComponent<Tile>();
    }

    public void DestroyTileMap()
    {
        foreach(List<Tile> tileList in tileMap.Values)
        {
            foreach(Tile t in tileList)
            {
                Destroy(t.gameObject);
            }
        }
    }

}