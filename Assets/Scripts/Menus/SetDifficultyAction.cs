using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class SetDifficultyAction : MenuAction
{
    public Difficulty Difficulty;
    
    public override void doAction()
    {
        GameState.Difficulty = Difficulty;
    }
}
