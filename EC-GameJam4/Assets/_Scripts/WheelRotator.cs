using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotator : Rotator
{

    private Animator _animator = null;
    

    protected override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
        
    }
 

    private void OnMouseDown()
    {
        //Debug.Log(gameObject.name);

        SetTargetQua(-1);
    }

    public override void Activate()
    {
        _animator.SetTrigger("Click");
    }





}
