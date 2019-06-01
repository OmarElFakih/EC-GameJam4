using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour //SINGLETON (does not survive between scenes)
{   
    public static EventManager manager = null;

    //event
    public delegate void Timer();
    public static Timer OnTimer;

    [SerializeField]
    private float timerIntervals = 0;

    private float nextInterval = 0;

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
        if (Time.time > nextInterval)
        {
            if (OnTimer != null)
            {
                OnTimer();
            }
            nextInterval = Time.time + timerIntervals;
        }
    }


}
