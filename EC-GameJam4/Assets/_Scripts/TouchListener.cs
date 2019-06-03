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
        if (Input.touchCount > 0)
        {
            foreach(Touch t in Input.touches)
            if (t.phase == TouchPhase.Began) {
                Debug.Log("tuch detected");
                if (Camera.main.ScreenToWorldPoint(t.position).x > 0)
                {
                    rotator1.SetTargetQua(-1);
                }
                else
                {
                    rotator2.SetTargetQua(1);
                }
            }
            
        }


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

        }
    }
}
