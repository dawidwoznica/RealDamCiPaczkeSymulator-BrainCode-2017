using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDestroyer : MonoBehaviour
{

    public GameObject explosion;

    private float _areaOfEffect = 5f;
    private Collider[] colls;
    public LayerMask CanBeDamagedMask;
    private AudioSource explosionSound;
    private bool isDestroyed;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Customer" && !isDestroyed)
        {
            isDestroyed = true;
            explosionSound = GetComponent<AudioSource>();
            StartCoroutine(Die());
            Destroy(other.gameObject);
        }
    }

    public IEnumerator Die()
    {
        Instantiate(explosion, transform.position, transform.rotation);

        yield return new WaitForSeconds(0.00001f);
        gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
    }
}