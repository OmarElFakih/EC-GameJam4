using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour //SINGLETON (does not survive between scenes)
{   
    public static EventManager manager = null; //used to ensure thath there is only 1 event manager at a time 

    //event
    public delegate void Timer(); // declaration of a delegate type named Timer
    public static Timer OnTimer;  // public event other classes can subscribe methods to


    [SerializeField]
    private Image _clockImage = null; // image showing amount of time left
    [SerializeField]
    private float _cT = 0f;
    private Color _clockColor = Color.white;


    [SerializeField]
    private float _timerIntervals = 0; //amount of time between events executions events

    private float _nextInterval = 0; //indicates wether or not is time to execute the event

    public float t; // timer intervals are lerped using t

    [SerializeField]
    private float _timerDelay = 0;

    private bool executed = false;

    private void Awake()
    {
        if (manager == null)
        {
            manager = this;
        }
        else if (manager != null && manager != this)
        {
            Destroy(this.gameObject);
        }
    }


    private void Start()
    {
        _nextInterval = Time.time + _timerIntervals; //ensures that the timer event is not executed inmedeatly
        _clockColor = _clockImage.color;
    }

    private void Update()
    {
        if (!GameManager.gameIsOver && !executed)
        {
            _clockImage.fillAmount = (_nextInterval - Time.time) / _timerIntervals;
        }

        if (CanExecute())
        {
            StartCoroutine(TimerRoutine());
            
        }

        ClockImageLerper();
    }


    public IEnumerator TimerRoutine()
    {
        _clockImage.color = new Color(_clockImage.color.a, _clockImage.color.b, _clockImage.color.g, 0);
        _clockImage.fillAmount = 1;
        executed = true;
        OnTimer();
        yield return new WaitForSeconds(_timerDelay);
        _timerIntervals = Mathf.Lerp(_timerIntervals, 2.7f, t * Time.deltaTime);
        _nextInterval = Time.time + _timerIntervals;
        executed = false;

    }

    private bool CanExecute()
    {
        return (OnTimer != null && Time.time > _nextInterval && !GameManager.gameIsOver && !executed);
    }


    public void ClockImageLerper()
    {
        _clockImage.color = Color.Lerp(_clockImage.color, _clockColor, _cT * Time.deltaTime);
    }

    public void Ready()
    {
        if(OnTimer != null && !executed)
        StartCoroutine(TimerRoutine());
    }

}
