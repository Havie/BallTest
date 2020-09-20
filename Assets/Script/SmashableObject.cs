using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;
using static Obi.ObiSolver;

public class SmashableObject : MonoBehaviour
{
    [SerializeField] ObiSolver _solver;

    [SerializeField] int _myHardness;

    private void Awake()
    {
        if (_solver==null)
            _solver = this.GetComponentInParent<ObiSolver>();
    }

    private void Start()
    {
       if(_solver!=null)   //Creates extreme lag 
            _solver.OnCollision += CallBack;
    }

    private void CallBack( ObiSolver solver, ObiCollisionEventArgs contacts)
    {
        var handles = ObiColliderWorld.GetInstance().colliderHandles;
        if (handles == null)
            return;
        if (handles.Count == 0)
            return;

        foreach ( Oni.Contact contact in contacts.contacts)
        {
            //If this runs too many times we get NPEs and I have no idea why
            if (contact.distance < 0.01)
            {
                ParticleInActor self = solver.particleToActor[contact.particle];
                ObiColliderHandle self2 = handles[contact.particle];
                ObiColliderHandle other = handles[contact.other];
                string name = self.gameObject;
                Debug.Log("SELF= " + self + "   name/GO:" +name);
                Debug.Log("self2= " + self2);
                Debug.Log("Other= " + other );
                /* Debug.Log("HIT= " + self.gameObject);
                 if (self.gameObject==this.gameObject)
                 {
                     Debug.Log("I WAS HIT " + this.gameObject);


                     ObiColliderBase other = handles[contact.other].owner;
                     break;
                 }*/
                
                //Stop Unity from freezing 
                Destroy(this.gameObject);
                break;
            }
        }
    }

    public void ImHit()
    {
        SpawnController.Instance.ObjectDied(this.gameObject);
    }


    private void DecideDamage(BallStats ball)
    {
        if (ball.ShouldTakeDamage(_myHardness))
            ball.TakeDamage(1); //What is my damage modifier?
    }
   
}
