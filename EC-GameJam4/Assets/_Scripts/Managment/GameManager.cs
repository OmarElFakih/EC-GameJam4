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

    [HideInInspector]
    public CanvasManager cManager = null;

    [SerializeField]
    private float _maxHealth = 0;

    private float _currentHealth = 0;


    [SerializeField]
    private AudioClip _tripleScoreClip = null;
    [SerializeField]
    private AudioClip _missClip = null;


    [HideInInspector]
    public int _currentScore = 0;

   /* [SerializeField]
    private int _mainGameIndex = 0; */

    public int maxscore = 0;
    private int _hits = 0;

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
    }

    public void GameStart()
    {
        gameIsOver = false;
        highScore = false;
        maxscore = PlayerPrefs.GetInt("MaxScore");
        _currentScore = 0;
        _currentHealth = _maxHealth;
        if (cManager != null)
        {
            cManager.UpdateBar(_currentHealth / _maxHealth);
            cManager.UpdateScore(_currentScore);
        }
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForEndOfFrame();
        gameIsOver = true;

        highScore = _currentScore > maxscore;
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

        _hits++;

        if (_hits >= 3)
        {
            AudioSource.PlayClipAtPoint(_tripleScoreClip, Camera.main.transform.position);
            _hits = 0;
        }

        if (CantProceed())
        {
            return;
        }

        _currentScore += amount;

         cManager.UpdateScore(_currentScore);

   
    }

    public void Miss()
    {
        if (_canPlayClip)
        {
            _canPlayClip = false;
            AudioSource.PlayClipAtPoint(_missClip, Camera.main.transform.position);
            StartCoroutine(RestoreBoolean());
        }


        if (CantProceed())
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
            StartCoroutine(GameOver());
        }

       
    }

    private IEnumerator RestoreBoolean()
    {
        yield return new WaitForSeconds(.2f);
        _canPlayClip = true;
    }


    private bool CantProceed()
    {

        return (debugging || gameIsOver);
    }

}
