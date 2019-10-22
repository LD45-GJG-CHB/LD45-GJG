namespace NewMainMenu.Base
{
    /// <summary>A base screen class that implements parameterless Show and Hide methods</summary>
    public abstract class AbstractScreen<T> : Screen<T> where T : AbstractScreen<T>
    {
        public static void Show() => Open();

        public static void Hide() => Close();
    }
}