using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerMovement : MonoBehaviour
{
    private Transform point;

    private NavMeshAgent nav;

	void Awake ()
	{
	    point = GameObject.FindGameObjectWithTag("Point").transform;
	    nav = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    nav.SetDestination(point.position);
	}
}
