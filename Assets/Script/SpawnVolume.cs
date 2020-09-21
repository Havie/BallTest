using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

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

        ToggleObiSoftBody(go, false);

        //RESET OBI Softbody Somehow

        //Move it
        go.transform.position = GenerateRandomLoc();
        //stand it back up
        go.transform.rotation = Quaternion.identity;

        ToggleObiSoftBody(go, true);

    }

    /** This method is needed otherwise you can't move them  */
    private void ToggleObiSoftBody(GameObject go, bool on)
    {
        ObiSoftbody obi = go.GetComponent<ObiSoftbody>();
        obi.enabled = on;
    }
    public Vector3 GenerateRandomLoc()
    {
        float x = Random.Range(-5, 5) + this.transform.position.x;
        float y = Random.Range(0, 5) + this.transform.position.y;
        float z = Random.Range(-5, 5) + this.transform.position.z;

        return new Vector3(x,y,z);
    }
}
