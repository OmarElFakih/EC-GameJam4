using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchRaycaster : MonoBehaviour
{
    [SerializeField]
    private bool OnMobile;

    public delegate void RaycastRoutine();
    public RaycastRoutine raycastRoutine;

    private void Start()
    {
        if (OnMobile)
        {
            raycastRoutine = TouchRaycast;
        }
        else
        {
            raycastRoutine = ClickRaycast;
        }
    }

    private void Update()
    {
        raycastRoutine?.Invoke();
    }


    public void TouchRaycast()
    {
        if (Input.touchCount == 0)
        {
            return;
        }

        foreach (Touch t in Input.touches)
        {
            if (t.phase == TouchPhase.Began)
            {
                Vector3 _touchPos = Camera.main.ScreenToWorldPoint(t.position);
                RaycastHit2D hit = Physics2D.Raycast(_touchPos, Vector2.zero);

                if (hit)
                {
                    //hit routine
                }

            }
        }
    }

    public void ClickRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(_mousePos, Vector3.zero);
            if (hit)
            {
                Debug.Log(hit.transform.name);

            }

        }
    }


}


