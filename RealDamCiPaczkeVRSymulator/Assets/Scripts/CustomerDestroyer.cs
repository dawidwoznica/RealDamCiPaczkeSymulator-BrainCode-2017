using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerDestroyer : MonoBehaviour {


    void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        Destroy(this);
    }
}
