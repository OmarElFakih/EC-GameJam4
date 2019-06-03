using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rotator : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 0;

    private Quaternion _targetQua = Quaternion.identity;


    private void OnEnable()
    {
        EventManager.OnTimer += Activate;
    }

    private void OnDisable()
    {
        EventManager.OnTimer -= Activate;

    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        _targetQua = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetQua, _rotationSpeed * Time.deltaTime);
    }

    public void SetTargetQua(int nTurns)
    {
        Vector3 _euler = new Vector3(0, 0, 45) * nTurns;
        _targetQua = Quaternion.Euler(_euler + _targetQua.eulerAngles);
    }

    public abstract void Activate();
}
