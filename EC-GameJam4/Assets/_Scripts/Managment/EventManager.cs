using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour //SINGLETON (does not survive between scenes)
{   
    public static EventManager manager = null;

    //event
    public delegate void Timer();
    public static Timer OnTimer;


    [SerializeField]
    private Image _clock = null;

    [SerializeField]
    private float timerIntervals = 0;

    private float nextInterval = 0;

    public float t;

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
        nextInterval = Time.time + timerIntervals;
    }

    private void Update()
    {
        if (!GameManager.gameIsOver)
        {
            _clock.fillAmount = (nextInterval - Time.time) / timerIntervals;
        }

        if (Time.time > nextInterval)
        {
            if (OnTimer != null && !GameManager.gameIsOver)
            {
                OnTimer();
                timerIntervals = Mathf.Lerp(timerIntervals, 0f, t * Time.deltaTime);
            }
            nextInterval = Time.time + timerIntervals;
        }
    }


}
