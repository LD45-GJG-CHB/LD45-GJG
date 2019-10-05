using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRunner : Singleton<GameRunner>
{

    public static List<string> mapNames = Maps.mapNames;
    public static int iterator = 1;

    public static void LoadNextLevel()
    {
        if (iterator == mapNames.Count)
        {
            Debug.Log("The End!");
            
            DOTween.Sequence()
                .SetUpdate(true)
                .AppendCallback((() => Time.timeScale = 0.0f))
                .Append(Player.Instance._darkness.DOFade(1.0f, 0.6f))
                .AppendInterval(1.0f)
                .AppendCallback((() => Time.timeScale = 1.0f))
                .AppendCallback(() => SceneManager.LoadScene("HighscoreInputScene"));
            return;
        }

        DOTween.Sequence()
            .SetUpdate(true)
            .AppendCallback((() => Time.timeScale = 0.0f))
            .Append(Player.Instance._darkness.DOFade(1.0f, 0.2f))
            .AppendInterval(0.1f)
            .AppendCallback(() =>
            {
                MapLoader.Instance.DestroyTileMap();
                MapLoader.Instance.currentMap = mapNames[iterator++];
                MapLoader.Instance.LoadNextLevel();
                Player.Instance.Velocity = Vector3.zero;
                Player.Instance.Velocity.x = Player.Instance._moveSpeed;
            })
            .Append(Player.Instance._darkness.DOFade(0.0f, 0.3f))
            .AppendInterval(0.2f)
            .AppendCallback((() => Time.timeScale = 1.0f))
            .Play();

    }
 
}
