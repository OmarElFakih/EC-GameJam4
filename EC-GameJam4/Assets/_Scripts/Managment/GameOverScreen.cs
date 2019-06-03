using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText = null;

    [SerializeField]
    private GameObject _highScoreText = null;

    private void OnEnable()
    {
        _scoreText.text = "final score: " + GameManager.manager._currentScore;
        if (GameManager.highScore)
        {
            _highScoreText.SetActive(true);
        }
    }
}
