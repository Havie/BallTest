using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Obi;
using static Obi.ObiSolver;

public class BallController : MonoBehaviour
{

    public enum eSelectionState { FREE, SELECTED };
    public eSelectionState _state;

    public Rigidbody _rb;
    private int BallFireForce = 200;

    [SerializeField] ObiSolver _solver;

    private List<SmashableObject> _collided = new List<SmashableObject>();
    private void Awake()
    {
        if (_solver == null)
            _solver = GameObject.FindObjectOfType<ObiSolver>();
    }
    private void Start()
    {
        if (_solver != null)
            _solver.OnCollision += CallBack;
    }

    private void CallBack(ObiSolver solver, ObiCollisionEventArgs contacts)
    {
        var handles = ObiColliderWorld.GetInstance().colliderHandles;
        if (handles == null)
            return;
        if (handles.Count == 0)
            return;


        foreach (Oni.Contact contact in contacts.contacts)
        {
            if (contact.distance < 0.01)
            {

                ObiColliderHandle other = handles[contact.other];

                 if (other.owner.gameObject==this.gameObject)
                {
                   // Debug.Log("FOUND THE DAMN BALL");

                    //Get the GameObject of collided object
                    ParticleInActor particleInActor = solver.particleToActor[contact.particle];
                    GameObject go = particleInActor.actor.gameObject;
                    SmashableObject so = go.GetComponent<SmashableObject>();

                    if (so)
                    {
                        //Tmp fix to handle an OnCollisionExit
                        if (!_collided.Contains(so))
                        {
                            //Debug.Log("BALL HIT CAN!!!");
                            if (!so.DecideDamage(this.GetComponent<BallStats>()))
                                so.ImHit(); //the ball didn't take damage 

                            StartCoroutine(CollisionExit(so));
                        }
                    }

                }
            }
        }
    }
    /**A tmp solution to minimize collision damage */
    private IEnumerator CollisionExit(SmashableObject so)
    {
        _collided.Add(so);
        yield return new WaitForSeconds(1);
        _collided.Remove(so);
    }
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

        if (Input.GetKeyDown(KeyCode.S))
            StopBall();
    }
    private void TickSelected()
    {
        Vector3 point = GetPointUnderMouse();

        Vector3 lineAngle = (point - transform.position) * -1;

        // GameObject.Instantiate(PREFAB, point, Quaternion.identity);


        Vector3 direction = new Vector3(lineAngle.x, this.transform.position.y, lineAngle.z).normalized;

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
        //_rb.AddForce(direction * BallFireForce, ForceMode.Impulse);
        _rb.velocity = direction * BallFireForce;
        //_rb.AddForceAtPosition(lineAngle*10, this.transform.position, ForceMode.Acceleration);
    }

    public void StopBall()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}
