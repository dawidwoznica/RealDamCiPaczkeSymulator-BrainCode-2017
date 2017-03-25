using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {
    
    public GameObject Customer;               
    public float SpawnTime = 7f;            
    public Transform[] SpawnPoints;         


    void Start()
    {
        InvokeRepeating("Spawn", SpawnTime, SpawnTime);
    }


    void Spawn()
    {
       
        int spawnPointIndex = Random.Range(0, SpawnPoints.Length);

        Instantiate(Customer, SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
    }
    
}
