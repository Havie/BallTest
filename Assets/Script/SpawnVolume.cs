using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVolume : MonoBehaviour
{

    public void Awake()
    {
        if (GetComponentInParent<SpawnController>() == null)
            Debug.LogWarning("You have a spawner that is not a child of SpawnController, wont be used");
    }

    public void SpawnObject(GameObject go)
    {
        go.SetActive(true);
        //RESET OBI Softbody Somehow

        go.transform.position = GenerateRandomLoc();
    }

    public Vector3 GenerateRandomLoc()
    {
        float x = Random.Range(-5, 5) + this.transform.position.x;
        float y = Random.Range(0, 5) + this.transform.position.y;
        float z = Random.Range(-5, 5) + this.transform.position.z;

        return new Vector3(x,y,z);
    }
}
