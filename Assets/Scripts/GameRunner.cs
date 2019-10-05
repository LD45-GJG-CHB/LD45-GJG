using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRunner : MonoBehaviour
{

    public List<string> mapNames = Maps.mapNames;
    MapLoader map = new MapLoader();

    void Awake()
    {
        map.currentMap = mapNames[0];
    }

    void Start()
    {
        
    }

 
}
