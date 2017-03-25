using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollController : MonoBehaviour
{

    public AudioSource _yell;

    void Awake()
    {
        _yell = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GameController"))
        {
            Debug.Log("HEH");
            _yell.Play();
        }

    }

    void OnCollisionExit(Collision other)
    {
        _yell.Stop();
    }
}
