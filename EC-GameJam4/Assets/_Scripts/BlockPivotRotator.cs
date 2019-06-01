using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPivotRotator : Rotator
{
    [SerializeField]
    private float _secondsToSpin = 0;

    public override void Activate()
    {
        StartCoroutine(TurnBlock());
    }

    private IEnumerator TurnBlock()
    {
        yield return new WaitForSeconds(_secondsToSpin);
        int _nTurns = Random.Range(-4, 4);
        SetTargetQua(_nTurns);
    }
}
