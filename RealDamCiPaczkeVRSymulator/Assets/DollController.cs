using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollController : MonoBehaviour
{

    private AudioSource _yell;

    void Awake()
    {
        _yell = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        _yell.Play();
    }

    void OnCollisionExit(Collision other)
    {
        _yell.Stop();
    }
}
