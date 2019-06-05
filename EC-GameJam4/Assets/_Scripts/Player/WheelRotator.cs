using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotator : Rotator
{

    private Animator _animator = null;

    [HideInInspector]
    public bool canTurn = true;

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
        
    }


    private void OnMouseDown()
    {
        if (Mayturn())
        {
            SetTargetQua(-1);
        }
    }


    public override void Activate()
    {
        _animator.SetTrigger("Click");
    }

    private bool Mayturn()
    {
        return (canTurn && Time.timeScale != 0 && !GameManager.gameIsOver && !GameManager.manager.android);
    }





}
