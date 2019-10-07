using System;

[System.Serializable]
public class Highscore
{
    public String name;
    public int score;
    public int place;
    public string difficulty;

    public Highscore() {}
    public Highscore(String name, int score, string diff)
    {
        this.name = name;
        this.score = score;
        this.difficulty = diff;
    }
}
