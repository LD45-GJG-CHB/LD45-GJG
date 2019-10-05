using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartEntry : MenuAction
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void doAction()
    {
        SceneManager.LoadScene("Siim", LoadSceneMode.Single);
    }
}
