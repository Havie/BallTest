using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform _currBall;
    private Vector3 _offset;


    private float _rotSpeed=41.3f;


    private void Start()
    {
        if(_currBall)
            _offset = this.transform.position - _currBall.position;
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.E))
            OrbitObject(true);
        else if (Input.GetKey(KeyCode.Q))
            OrbitObject(false);
        else if (_currBall)
            StayInView();
    }
    private void GetOffset()
    {
        _offset = this.transform.position - _currBall.position;
    }
    private void LookAtBall()
    {
        this.transform.LookAt(_currBall);
    }
    public void StayInView()
    {
       this.transform.position = _currBall.position + _offset;
        LookAtBall();
    }

    private void OrbitObject(bool right)
    {
        if(right)
            transform.RotateAround(_currBall.position, Vector3.up, _rotSpeed * Time.deltaTime);
        else
            transform.RotateAround(_currBall.position, Vector3.up,-1* _rotSpeed * Time.deltaTime);

        LookAtBall();
        GetOffset();
    }
}
