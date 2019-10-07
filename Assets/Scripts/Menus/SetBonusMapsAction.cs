using UnityEngine.SceneManagement;

public class SetBonusMapsAction : MenuAction
{
    public bool bonusMaps;
    public override void doAction()
    {
        GameState.isBonusMaps = bonusMaps;
        
    }
}
