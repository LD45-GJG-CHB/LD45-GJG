using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapLoader : Singleton<MapLoader>
{
    public string currentMap;

    public GameObject tilePrefab;
    public GameObject exitPrefab;
    public GameObject edgeTilePrefab;
    public float tileSize = 2;
    public Dictionary<string, List<Tile>> tileMap;
    private string[,] map;
    
    public void LoadNextLevel()
    {
        tileMap = new Dictionary<string, List<Tile>>();

        var _map = Maps.maps[currentMap];
        int sizeX = Maps.GetColumnAmount(_map);
        int sizeY = Maps.GetRowAmount(_map);
        map = Maps.StringArrayTo2DArray(_map);

        for (var y = 0; y < sizeY; y++)
        {
            for (var x = 0; x < sizeX; x++)
            {
                var letter = map[x, y];

                Tile tile = null;
                switch (letter)
                {
                    case "1": // player pos
                        var posX = x * tileSize;
                        var posY = y * -tileSize;
                            
                        Player.Instance.transform.position = new Vector3(posX, posY, 0);
                        break;
                    case "-": // nothingness
                        break;
                    case " ":
                        break;
                    case "@": // exit / door
                        tile = CreateTileAtPosition(x, y);

                        tile.GetComponent<Tile>().tileype = TileType.DOOR;

                        tile.GetComponent<BoxCollider2D>().isTrigger = true;

                        tile.gameObject.layer = 11;
                        var rb = tile.gameObject.AddComponent<Rigidbody2D>();
                        rb.gravityScale = 0.0f;
                        rb.isKinematic = true; 
                        tile.GetComponent<Tile>().SetLetter(letter.ToLower());


                        break;
                    default: // default letter tile
                        tile = CreateTileAtPosition(x, y);

                        tile.GetComponent<Tile>().SetLetter(letter.ToLower());

                        break;
                }
                
                if (tileMap.TryGetValue(letter.ToLower(), out var tiles))
                {
                    tiles.Add(tile);    
                }
                else
                {
                    tileMap[letter.ToLower()] = new List<Tile> {tile};
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
        tileMap?.Values
            .SelectMany(tm => tm).ToList()
            .ForEach(tile =>
            {
                if (!tile)
                    return;

                DestroyImmediate(tile.gameObject);
            });
    }

}