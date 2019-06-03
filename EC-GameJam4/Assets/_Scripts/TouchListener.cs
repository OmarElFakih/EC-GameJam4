using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchListener : MonoBehaviour
{
    public WheelRotator rotator1;
    public WheelRotator rotator2;

    // Update is called once per frame
    void Update()
    {

        if (Time.timeScale == 0)
        {
            return;
        }

        if (Input.touchCount > 0)
        {
            foreach (Touch t in Input.touches)
            {
                Vector3 _tPosition = Camera.main.ScreenToWorldPoint(t.position); 

                if (t.phase == TouchPhase.Began && _tPosition.y < 15)
                {

                    if (_tPosition.x > 0)
                    {
                        rotator1.SetTargetQua(-1);
                    }
                    else
                    {
                        rotator2.SetTargetQua(1);
                    }
                }
            }
            
        }
        /*

        if (Input.GetMouseButtonDown(0))
        {
            
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > 0)
            {
                rotator1.SetTargetQua(-1);
            }
            else
            {
                rotator2.SetTargetQua(1);
            }

        }*/
    }
}
