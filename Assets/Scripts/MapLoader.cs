using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{

    public string currentMap { get; set; } = "map_0"; // Somehow edit this variable between levels
    public string currentFont { get; set; } = "font_0"; // Somehow edit this variable between levels

    public GameObject tilePrefab;
    public float tileSize = 1;

    private string[,] map;
    private Dictionary<string, GameObject> letterTiles;

    public void Awake()
    {
        map = Maps.StringTo2DArray(Maps.maps[currentMap]);
        letterTiles = new Dictionary<string, GameObject>();
        foreach (var letter in alphabet())
        {
            // Peab gameobjectina laadima gameobjektid, mitte png-sid bljat
//            var letterTile = Instantiate(tilePrefab);
//            letterTiles.Add(letter, letterTile);
        }

        Debug.Log(map[0,0]);

        for (var y = 0; y < Maps.mapSizeY; y++)
        {
            for (var x = 0; x < Maps.mapSizeX; x++)
            {    
                var letter = map[x,y];
                var tile = Instantiate(tilePrefab, transform);

                var posX = x * tileSize;
                var posY = y * -tileSize;

                tile.transform.position = new Vector2(posX, posY);
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
