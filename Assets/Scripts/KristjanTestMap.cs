using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KristjanTestMap : MonoBehaviour
{

    /**
     * Menüüs peaks laadima kõik Maps klassist saama kõik levelid
     * Need peaksid olema listis, et oleksid järjestatud (seega ei saa võtta Maps.maps key-sid)
     */

    public string currentMap { get; set; } = "map_0"; // Somehow edit this variable between levels
    public string currentFont { get; set; } = "font_0"; // Somehow edit this variable at the start menu of something
    
    public float tileSize = 1;

    private List<List<string>> map;
    private Dictionary<string, GameObject> letterTiles;

    public GameObject tilePrefab_;

    public void Awake()
    {
        

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
        map = Maps.convertStringMapToNestedList(Maps.maps[currentMap]);

        for (var y = 0; y < Maps.mapSizeY - 1; y++)
        {
            for (var x = 0; x < Maps.mapSizeX - 1; x++)
            {
                var posX = x * tileSize;
                var posY = y * -tileSize;
                Instantiate(TilePrefab_, new Vector2(posX, posY), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
