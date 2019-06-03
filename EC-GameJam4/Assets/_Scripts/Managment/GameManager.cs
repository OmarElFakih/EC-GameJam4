using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour //SINGLETON
{
    // static reference to itself
    public static GameManager manager = null;
    public static bool gameIsOver = false;
    public static bool highScore = false;

    public bool debugging;
    public bool android = false;

    [HideInInspector]
    public CanvasManager cManager = null;

    [SerializeField]
    private float _maxHealth = 0;

    private float _currentHealth = 0;


    [SerializeField]
    private AudioClip _fullScoreClip = null;
    [SerializeField]
    private AudioClip _missClip = null;


    [HideInInspector]
    public int _currentScore = 0;

   /* [SerializeField]
    private int _mainGameIndex = 0; */

    private int _maxscore = 0;
    private int _hits = 0;
    private int _hitsToFullScore = 3;

    private bool _canPlayClip = true;



    //turns the first instance of this class into a singleton
    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (manager != null && manager != this)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        GameStart();
        _hitsToFullScore = android ? 2 : 3;
    }

    public void GameStart()
    {
        gameIsOver = false;
        highScore = false;
        _maxscore = PlayerPrefs.GetInt("MaxScore");
        _currentScore = 0;
        _currentHealth = _maxHealth;
        if (cManager != null)
        {
            cManager.UpdateBar(_currentHealth / _maxHealth);
            cManager.UpdateScore(_currentScore);
        }
    }

    public void GameOver()
    {
        gameIsOver = true;

        highScore = _currentScore > _maxscore;
        if (highScore)
        {
            PlayerPrefs.SetInt("MaxScore", _currentScore);
        }

        cManager.gameoverScreen.SetActive(true);

       // Time.timeScale = 0;

    }

   /* public void Restart()
    {
        LoadScene(_mainGameIndex);
        GameStart();
    }*/


    public void TimeStop()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
    }


    public void LoadScene(int index)
    {
        Time.timeScale = 1;
        GameStart();
        SceneManager.LoadScene(index);
        
    }


    public void AddScore(int amount)
    {
        if (debugging)
        {
            return;
        }

        _currentScore += amount;
         cManager.UpdateScore(_currentScore);

        _hits++;

        if (_hits >= _hitsToFullScore)
        {
            AudioSource.PlayClipAtPoint(_fullScoreClip, Camera.main.transform.position);
            _hits = 0;
        }
    }

    public void Miss()
    {
        if (debugging)
        {
            return;
        }

        _hits = 0;
        if (_currentHealth > 0)
        {
            _currentHealth--;
            cManager.UpdateBar(_currentHealth / _maxHealth);
        }

        if (_currentHealth == 0)
        {
            GameOver();
        }

        if (_canPlayClip)
        {
            _canPlayClip = false;
            AudioSource.PlayClipAtPoint(_missClip, Camera.main.transform.position);
            StartCoroutine(RestoreBoolean());
        }
       
    }

    private IEnumerator RestoreBoolean()
    {
        yield return new WaitForSeconds(.2f);
        _canPlayClip = true;
    }

}
