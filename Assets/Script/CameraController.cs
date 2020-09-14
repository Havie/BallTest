using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _currBall;
    private Vector3 _offset;

    private void Start()
    {
        if(_currBall)
            _offset = this.transform.position - _currBall.position;
    }

    private void LateUpdate()
    {
        if (_currBall)
            StayInView();
    }

    public void StayInView()
    {
        this.transform.position = _currBall.position + _offset;
        this.transform.LookAt(_currBall);
        
    }
}
