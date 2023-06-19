using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private int _totalScore;
    
    private void Start()
    {
        UpdateScoreText();
    }
    
    public void SetScore(int score)
    {
        _totalScore = score;
        UpdateScoreText();
    }
	
    public void ResetScore()
    {
        _totalScore = 0;
        UpdateScoreText();
    }
	
    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = _totalScore.ToString();
    }
}