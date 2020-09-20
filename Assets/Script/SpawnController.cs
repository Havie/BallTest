using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

    public SpawnVolume[] _volumes;

    #region singleton
    public static SpawnController Instance {get;private set;}

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
        {
            Debug.LogWarning("Duplicate _instance, destroying : " + this.gameObject);
            Destroy(this);
        }

    }
    #endregion
    private void Start()
    {
        _volumes = GetComponentsInChildren<SpawnVolume>();
    }


    /** Let us know somethings been Destroyed */
    public void ObjectDied(GameObject smashableObject)
    {
        StartCoroutine(RespawnCD(smashableObject));
    }
    /** Start a delay by turning it off */
    IEnumerator RespawnCD(GameObject smashableObject)
    {
        yield return new WaitForSeconds(1);
        smashableObject.SetActive(false);
        yield return new WaitForSeconds(5);
        RespawnObject(smashableObject);
    }
    /**Tell one of our spawners to reuse this object via pooling */
    private void RespawnObject(GameObject smashableObject)
    {
        int rng = Random.Range(0, _volumes.Length-1);
        _volumes[rng].SpawnObject(smashableObject);
    }
}
