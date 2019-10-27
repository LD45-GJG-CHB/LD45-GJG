﻿using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NewMainMenu.Base
{
	public class MenuManager : Singleton<MenuManager>
	{
		[Header("Menu screens")]
//		public PauseMenu pauseMenuPrefab; TODO
		public TitleScreen titleScreenPrefab;
		public OptionsScreen optionsScreenPrefab;
		public DifficultySelectionScreen difficultySelectionScreenPrefab;
		public FontSelectionScreen fontSelectionScreenPrefab;
		public ThemeSelectionScreen themeSelectionScreenPrefab;

		private readonly Stack<Screen> _screens = new Stack<Screen>();


		private void Awake()
		{
			if (SceneManager.GetActiveScene().name == "Menu")
			{
				TitleScreen.Show();
			}
		}

		public void CreateInstance<T>() where T : Screen
		{
			var prefab = GetPrefab<T>();

			Instantiate(prefab, transform);
		}

		public void OpenScreen(Screen instance)
		{
			// De-activate top screen
			if (_screens.Count > 0)
			{
				if (instance.disableScreensUnderneath)
				{
					foreach (var screen in _screens)
					{
						screen.gameObject.SetActive(false);

						if (screen.disableScreensUnderneath)
							break;
					}
				}

				var topCanvas = instance.GetComponent<Canvas>();
				var previousCanvas = _screens.Peek().GetComponent<Canvas>();
				topCanvas.sortingOrder = previousCanvas.sortingOrder + 1;
			}
			
			_screens.Push(instance);
		}
		
		
		private T GetPrefab<T>() where T : Screen
		{
			// Get prefab dynamically, based on public fields set from Unity
			// You can use private fields with SerializeField attribute too
			var fields = GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
			foreach (var field in fields)
			{
				var prefab = field.GetValue(this) as T;
				if (prefab != null)
				{
					return prefab;
				}
			}

			throw new MissingReferenceException("Prefab not found for type " + typeof(T));
		}
	
		public void CloseScreen(Screen screen)
		{
			if (_screens.Count == 0)
			{
				Debug.LogErrorFormat(screen, "{0} cannot be closed because the screen stack is empty", screen.GetType());
				return;
			}

			if (_screens.Peek() != screen)
			{
				Debug.LogErrorFormat(screen, "{0} cannot be closed because it is not on top of stack", screen.GetType());
				return;
			}

			CloseTopScreen();
		}

		public void CloseTopScreen()
		{
			var instance = _screens.Pop();

			if (instance.destroyWhenClosed)
				Destroy(instance.gameObject);
			else
				instance.gameObject.SetActive(false);

			// Re-activate top screen
			// If a re-activated screen is an overlay we need to activate the screen under it
			foreach (var screen in _screens)
			{
				screen.gameObject.SetActive(true);

				if (screen.disableScreensUnderneath)
					break;
			}
		}

		private void Update()
		{
			// On Android the back button is sent as Esc
			if (Input.GetKeyDown(KeyCode.Escape) && _screens.Count > 0)
			{
				_screens.Peek().OnBackPressed();
			}
		}
	}
}
