using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public enum eSelectionState { FREE, SELECTED };
    public eSelectionState _state;

    public Rigidbody _rb;
    private int BallFireForce = 40;

    public GameObject PREFAB;
    public int distance = 1;

    private void Update()
    {

        if (_state == eSelectionState.FREE)
        {
            TickFree();
        }
        else if (_state == eSelectionState.SELECTED)
        {
            TickSelected();
        }
    }
    private void TickSelected()
    {
        Vector3 point = GetPointUnderMouse();

        Vector3 lineAngle = (point - transform.position) *-1;
        //Vector3 lineAngle = this.transform.position - point;

        // GameObject.Instantiate(PREFAB, point, Quaternion.identity);


        Vector3 direction = new Vector3(lineAngle.x, this.transform.position.y, lineAngle.z).normalized ;

        Debug.DrawLine(this.transform.position, this.transform.position + lineAngle, Color.red, 10);


        if (Input.GetMouseButtonUp(0))
        {
            ShootBallInDirection(direction);
            _state = eSelectionState.FREE;
        }
    }
    private void TickFree()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
                _state = eSelectionState.SELECTED;
            }
        }
    }
    private Vector3 GetPointUnderMouse()
    {
        //draw line
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, float.PositiveInfinity);
        return hit.point;
    }

    private void ShootBallInDirection(Vector3 direction)
    {
        //Shoot the ball
        print("Shooting ball twards: " + direction);
        _rb.AddForce(direction * BallFireForce, ForceMode.Impulse);
        //_rb.AddForceAtPosition(lineAngle*10, this.transform.position, ForceMode.Acceleration);
    }
}
