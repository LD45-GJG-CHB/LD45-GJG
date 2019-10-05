using UnityEditor;

public class SwitchPageAction : MenuAction
{
    public MenuPage Page;
    public MenuManager MenuManager;

    public override void doAction()
    {
        MenuManager.Switch(Page);
    }
}
