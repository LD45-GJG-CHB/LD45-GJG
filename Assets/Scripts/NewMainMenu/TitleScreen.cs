using NewMainMenu.Base;
using Themes;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace NewMainMenu
{
    public class TitleScreen : AbstractScreen<TitleScreen>
    {
        private void Start()
        {
            SetDefaultLook();
            AudioManager.Instance.Play("MainMenu");
        }

        private static void SetDefaultLook()
        {
            ThemeManager.Instance.SetCurrentTheme(ThemeType.MATRIX);
            ThemeManager.Instance.UpdateTheme();
        }

        public void OnStartPressed()
        {
            DifficultySelectionScreen.Show();
            GameState.isBonusMaps = false;
        }

        public void OnBonusMapsPressed()
        {
            DifficultySelectionScreen.Show();
            GameState.isBonusMaps = true;
        }

        public void OnMapEditorPressed()
        {
            SceneManager.LoadScene("MapEditor");
        }
        
        public void OnOptionsPressed()
        {
            OptionsScreen.Show();
        }

        public void OnExitPressed()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}