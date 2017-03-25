using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSpawnerController : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    public GameObject Customer;
    public Transform[] SpawnPoints;


    void Update()
    {
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            Spawn();
        }
    }

    void Spawn()
    {

        int spawnPointIndex = Random.Range(0, SpawnPoints.Length);

        Instantiate(Customer, SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
    }

}
