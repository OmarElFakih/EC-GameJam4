using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 0;


    private Animator _animator = null;
    private Quaternion _targetQua = Quaternion.identity;

    private void OnEnable()
    {
        EventManager.OnTimer += Activate;
    }

    private void OnDisable()
    {
        EventManager.OnTimer -= Activate;

    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _targetQua = transform.rotation;
    }

    private void Update()
    {
       // Vector3 _current = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetQua,_rotationSpeed * Time.deltaTime);
    }

    private void OnMouseDown()
    {
        //Debug.Log(gameObject.name);

        _targetQua = Quaternion.Euler(new Vector3(0, 0, -45) + _targetQua.eulerAngles);
    }

    public void Activate()
    {
        _animator.SetTrigger("Click");
    }





}
