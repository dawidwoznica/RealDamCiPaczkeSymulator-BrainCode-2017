using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    private Transform point;

    private NavMeshAgent nav;

    private AudioSource hey;

    private bool isPlaying = false;


	void Awake ()
	{
	    point = GameObject.FindGameObjectWithTag("Point").transform;
	    nav = GetComponent<NavMeshAgent>();
	    hey = GetComponent<AudioSource>();
        
    }
	
	
	void Update ()
	{
	    nav.SetDestination(point.position);
	    ShouldPlaySound();
        
    }

    void ShouldPlaySound()
    {
        if (Vector3.Distance(transform.position, point.position) < 1f && !isPlaying)
        {
            hey.Play();
            isPlaying = true;
        }
    }
 
}
