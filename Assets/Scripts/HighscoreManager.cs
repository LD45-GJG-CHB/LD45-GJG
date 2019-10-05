using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public GameObject highscoreEntry;
    private Highscore[] _highscores;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HighScoreAPI.GetTopList((responseText) =>
        {
            var response = JsonUtility.FromJson<HighscoreManager.Response>(responseText);
            _highscores = response.data;
        }));
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        if (_highscores != null && _highscores.Length > 0)
        {
            int index = 1;
            foreach (var highscore in _highscores)
            {
                InstantiateEntry(highscore, index++);
            }
        }
    }

    private void InstantiateEntry(Highscore highscore, int index)
    {
        GameObject entry = Instantiate(highscoreEntry, transform);
        var component = entry.GetComponent<HighscoreEntry>();
        component.nameText.text = $"{index.ToString()}. {highscore.name}";
        component.scoreText.text = highscore.score.ToString();
    }

    [System.Serializable]
    public class Response
    {
        public Highscore[] data;
    }
}
