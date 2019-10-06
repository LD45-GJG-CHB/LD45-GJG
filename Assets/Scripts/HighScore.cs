using System;

[System.Serializable]
public class Highscore
{
    public String name;
    public int score;
    public int place;
    
    public Highscore() {}
    public Highscore(String name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
