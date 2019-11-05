using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDifficultyAction : MenuAction
{
    public DifficultyLevel difficultyLevel;
    
    public override void doAction()
    {
        GameState.CurrentDifficulty = difficultyLevel.GetMatchingDifficulty();
    }
}
