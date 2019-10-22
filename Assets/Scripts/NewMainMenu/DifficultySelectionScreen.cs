using System;
using NewMainMenu.Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NewMainMenu
{
    public class DifficultySelectionScreen : AbstractScreen<DifficultySelectionScreen>
    {
        public void OnDifficultySelected(string difficulty) 
        {
            switch (difficulty.Trim().ToLowerInvariant())
            {
                case "easy":
                    GameState.Difficulty = Difficulty.EASY;
                    break;
                case "medium":
                    GameState.Difficulty = Difficulty.MEDIUM;
                    break;
                case "hard":
                    GameState.Difficulty = Difficulty.HARD;
                    break;
                case "penultimate":
                    GameState.Difficulty = Difficulty.PENULTIMATE_MAMBO_JAMBO;
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Difficulty not defined in script {nameof(DifficultySelectionScreen)} but is added to the screen.");
            }

            SceneManager.LoadScene("GameScene");
        }
    }
}
