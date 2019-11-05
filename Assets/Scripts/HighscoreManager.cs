using UnityEngine;

public class HighscoreManager : MonoBehaviour
{
    public GameObject highscoreEntry;
    private Highscore[] _highscores;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HighScoreApi.GetTopList((responseText) =>
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
            foreach (var highscore in _highscores)
            {
                InstantiateEntry(highscore);
            }
        }
    }

    private void InstantiateEntry(Highscore highscore)
    {
        GameObject entry = Instantiate(highscoreEntry, transform);
        var component = entry.GetComponent<HighscoreEntry>();
        component.nameText.text = $"{highscore.place.ToString()}. {highscore.name}";
        component.scoreText.text = highscore.score.ToString();
        component.diffText.text = string.IsNullOrWhiteSpace(highscore.difficulty) ? "-" : highscore.difficulty;
    }

    [System.Serializable]
    public class Response
    {
        public Highscore[] data;
    }
}
