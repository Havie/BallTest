using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO connect this data to softbody data

public class BallData : ScriptableObject
{
    [SerializeField] int _density;
    [SerializeField] float _maxVelocity;
    [SerializeField] int _weight;
    [SerializeField] int _hardness; //hp

    public int GetDensity() => _density;
    public float GetMaxVelocity() => _maxVelocity;
    public int GetWeight() => _weight;
    public int GetHardness() => _hardness;
}
