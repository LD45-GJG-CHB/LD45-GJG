using System;
using System.Linq;
using NewMainMenu.Base;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NewMainMenu
{
    public class DifficultySelectionScreen : AbstractScreen<DifficultySelectionScreen>
    {
        [SerializeField] private GameObject listItemPrefab;
        [SerializeField] private GameObject itemList;

        protected override void Awake()
        {
            FillDifficultyList();
            base.Awake();
        }

        private void FillDifficultyList()
        {
            var difficulties =
                DifficultyManager.GetAllDifficulties()
                    .ToList()
                    .OrderByDescending(diff => diff.moveSpeed);

            foreach (var diff in difficulties)
            {
                var go = Instantiate(listItemPrefab, itemList.transform);
                go.GetComponent<TextMeshProUGUI>().SetText(diff.difficultyName);
                go.GetComponent<Button>().onClick.AddListener(() => OnDifficultySelected(diff));
                go.transform.SetAsFirstSibling();
            }
        }

        public static void OnDifficultySelected(Difficulty difficulty)
        {
            GameState.CurrentDifficulty = difficulty;
            SceneManager.LoadScene("GameScene");
        }
    }
}