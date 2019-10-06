public class HideMenuAction : MenuAction
{
    public MenuDisplayer MenuDisplayer;
    
    public override void doAction()
    {
        MenuDisplayer.SetInvisible();
    }
}
