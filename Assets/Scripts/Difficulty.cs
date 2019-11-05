using UnityEngine;

[CreateAssetMenu(fileName = "New Difficulty", menuName = "Difficulty/New Difficulty", order = 1)]
public class Difficulty : ScriptableObject
{
    public string difficultyName = "NotNamed";
    public int moveSpeed = 13;
    public DifficultyLevel difficultyLevel = DifficultyLevel.MEDIUM;
}

public enum DifficultyLevel
{
    EASY = 1,
    MEDIUM,
    HARD,
    PENULTIMATE_MAMBO_JAMBO
}