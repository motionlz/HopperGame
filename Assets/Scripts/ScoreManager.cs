using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;
    private int highScore;
    public event Action<int> OnScoreChanged;
    public void Init()
    {
        ResetScore();
        GameManager.Instance.PlayerManager.PlayerActionModule.OnLanded += AddScore;
    }
    private void ResetScore()
    {
        score = 0;
        UpdateUI();
    }
    public void AddScore()
    {
        score++;
        UpdateUI();
    }

    private void UpdateUI()
    {
        OnScoreChanged?.Invoke(score);
    }
}
