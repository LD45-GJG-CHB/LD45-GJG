using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : Singleton<Score>
{
    private int _scoreValue;

    public void ResetScore(int initValue = 0)
    {
        _scoreValue = initValue;
    }

    public void IncrementScore(int value = 1)
    {
        _scoreValue += value;
    }
    
    public void DecrementScore(int value = 1)
    {
        _scoreValue -= value;
    }

    public int GetScore()
    {
        return _scoreValue;
    }
}
