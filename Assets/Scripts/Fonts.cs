using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fonts
{

    public static Dictionary<string, string> fonts;

    static Fonts()
    {
        //key = just for reference, value = name of actual folder
        fonts = new Dictionary<string, string>();
        fonts.Add("font_0", "Font0"); 
    }
  
}
