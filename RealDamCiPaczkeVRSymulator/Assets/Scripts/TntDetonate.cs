using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TntDetonate : MonoBehaviour
{


    public GameObject explosion;

    private float _areaOfEffect = 5f;
    private Collider[] colls;
    public LayerMask CanBeDamagedMask;
    private AudioSource explosionSound;

    void OnTriggerEnter(Collider other)
    {
        explosionSound = GetComponent<AudioSource>();

        colls = Physics.OverlapSphere(transform.position, _areaOfEffect, CanBeDamagedMask);

        for (int i = 0; i < colls.Length; i++)
        {
            Destroy(colls[i].gameObject);
        }

        Instantiate(explosion, transform.position, transform.rotation);
        explosionSound.Play();
     
        Destroy(explosion, 2f);
    }
}
