using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStats : MonoBehaviour
{
    [SerializeField] BallData _stats;


    [SerializeField] int _density;
    [SerializeField] float _maxVelocity;
    [SerializeField] int _weight;
    [SerializeField] int _hardness; //hp



    public int GetDensity() => _density;
    public float GetMaxVelocity() => _maxVelocity;
    public int GetWeight() => _weight;
    public int GetHardness() => _hardness;

    Vector3 _startLoc;

    private void Awake()
    {
        _startLoc = this.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            Die();
        if (Input.GetKeyDown(KeyCode.R))
            ResetBall();
    }

    public void SetBall(BallData ball)
    {
        _stats = ball;

        _density = _stats.GetDensity();
        _maxVelocity = _stats.GetMaxVelocity();
        _weight = _stats.GetWeight();
        _hardness = _stats.GetHardness();
    }


    public bool ShouldTakeDamage(int ObjectHitsStrength)
    {
        return ObjectHitsStrength>_density;
    }

    public void TakeDamage(int enemyStrength)
    {
        //Not sure on how damage is supposed to work need to clarify 
        int damage = Mathf.Clamp( enemyStrength- _density, 1, enemyStrength);
        _hardness -= damage;
        if (_hardness < 0)
            Die();
    }

    public void Die()
    {
        Debug.LogWarning("You Died");
        this.GetComponent<MeshRenderer>().enabled = false;
        BallController bc = this.GetComponent<BallController>();
        bc.StopBall();
        bc.enabled = false;


    }

    //Respawns the ball
    public void ResetBall()
    {
        this.GetComponent<MeshRenderer>().enabled = true;
        this.GetComponent<BallController>().enabled = true;
        this.transform.position = _startLoc;
    }

}
