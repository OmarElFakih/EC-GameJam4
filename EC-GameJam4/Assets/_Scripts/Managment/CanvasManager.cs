using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    private GameManager GM = null;

    [SerializeField]
    private TextMeshProUGUI _scoreText = null;

    [SerializeField]
    private Image _barFill = null;

    
    public GameObject gameoverScreen = null;

    [SerializeField]
    private GameObject[] _pausedGameElements = null;

    [SerializeField]
    private GameObject[] _unpausedGameElements = null;


    private void Awake()
    {
        GM = GameManager.manager;
        GM.cManager = this;
    }


    public void Load(int index)
    {
        GM.LoadScene(index);
    }

   /* public void ResetGame()
    {
        GM.Restart();
    }*/


    public void UpdateScore(int score)
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score: " + score;
        }
    }

    public void UpdateBar(float amount)
    {
        if (_barFill == null)
            return;

        _barFill.fillAmount = amount;
    }

    public void PauseRoutine()
    {
        if (Time.timeScale == 1)
        {
            ActivateElements(_unpausedGameElements, false);
            ActivateElements(_pausedGameElements, true);
        }
        else
        {
            ActivateElements(_unpausedGameElements, true);
            ActivateElements(_pausedGameElements, false);
        }

        GM.TimeStop();

    }


    private void ActivateElements(GameObject[] elements, bool state)
    {
        foreach (GameObject g in elements)
        {
            g.SetActive(state);
        }
    }

}
