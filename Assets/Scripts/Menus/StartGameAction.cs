using UnityEngine.SceneManagement;

public class StartGameAction : MenuAction
{
    public bool bonusMaps;
    public override void doAction()
    {
        GameState.isBonusMaps = bonusMaps;
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
