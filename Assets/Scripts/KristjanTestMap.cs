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
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
