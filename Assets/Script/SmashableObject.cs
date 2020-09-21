using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SmashableObject : MonoBehaviour
{
   

    [SerializeField] int _myHardness;

    public void ImHit()
    {
        SpawnController.Instance.ObjectDied(this.gameObject);
    }


    public bool DecideDamage(BallStats ball)
    {
        if (ball.ShouldTakeDamage(_myHardness))
        {
            ball.TakeDamage(1); //What is my damage modifier?
            return true;
        }
        return false;
    }
   
}
