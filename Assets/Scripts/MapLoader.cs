using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{

    public string currentMap { get; set; } = "map_0"; // Somehow edit this variable between levels
    public string currentFont { get; set; } = "font_0"; // Somehow edit this variable between levels
    public float tileSize = 1;

    private List<List<string>> map;
    private Dictionary<string, GameObject> letterTiles;

    public void Awake()
    {
        this.map = Maps.convertStringMapToNestedList(Maps.maps[currentMap]);
        letterTiles = new Dictionary<string, GameObject>();
        foreach (string letter in alphabet())
        {
            // Peab gameobjectina laadima gameobjektid, mitte png-sid bljat
            Debug.Log(Fonts.fonts[currentFont] + "/" + letter + ".png");
            GameObject letterTile = (GameObject)Instantiate(Resources.Load(Fonts.fonts[currentFont] + "/" + letter + ".png"));
            letterTiles.Add(letter, letterTile);
        }

        Debug.Log(map[0][0]);

        for (var y = 0; y < Maps.mapSizeY - 1; y++)
        {
            for (var x = 0; x < Maps.mapSizeX - 1; x++)
            {
                string letter = map[x][y];
                GameObject tile = (GameObject)Instantiate(letterTiles[letter], transform);

                float posX = x * tileSize;
                float posY = y * -tileSize;

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
