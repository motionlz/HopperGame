using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Score UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    
    [Header("End UI")]
    [SerializeField] private GameObject gameOverUI;

    public void Init()
    {
        GameManager.Instance.ScoreManager.OnScoreChanged += UpdateScoreText;
        GameManager.Instance.PlayerManager.OnDeath += () => ShowEndUI();
    }

    private void UpdateScoreText(int _score)
    {
        scoreText.text = _score.ToString();
    }

    private void ShowEndUI(bool _isShow = true)
    {
        gameOverUI.SetActive(_isShow);
    }
}
