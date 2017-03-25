using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Something : MonoBehaviour {

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Customer")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}