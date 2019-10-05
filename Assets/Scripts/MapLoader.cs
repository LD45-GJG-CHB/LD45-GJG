using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : Singleton<MapLoader>
{
    public string currentMap { get; set; } = "map_0"; // Somehow edit this variable between levels
    public string currentFont { get; set; } = "font_0"; // Somehow edit this variable between levels

    public GameObject tilePrefab;
    public GameObject exitPrefab;
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
                var letter = map[x,y];

                if (letter == "1")
                {
                    Player._instance.transform.position = new Vector3(x,y,0);
                }
                if (letter == "2")
                {
                    var tile = Instantiate(exitPrefab, new Vector3(x,y), Quaternion.identity,transform);
                }
                else
                {
                    var tile = Instantiate(tilePrefab, transform);

                    var posX = x * tileSize;
                    var posY = y * -tileSize;

                    tile.transform.position = new Vector2(posX, posY);
                
                    tile.GetComponent<Tile>().SetLetter(letter);
                }

            }
        }

    }

    private List<string> alphabet()
    {
        List<string> alphabet = new List<string>();
        alphabet.Add("a");
        alphabet.Add("b");
        return alphabet;
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
