using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDetonate : MonoBehaviour
{
    public GameObject explosion;

    private float _areaOfEffect = 5f;
    private Collider[] colls;
    public LayerMask CanBeDamagedMask;
    private AudioSource explosionSound;
    public AudioClip destroySound;
    private bool isDestroyed;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Customer" && !isDestroyed)
        {
            isDestroyed = true;
            explosionSound = GetComponent<AudioSource>();

            colls = Physics.OverlapSphere(transform.position, _areaOfEffect, CanBeDamagedMask);

            for (int i = 0; i < colls.Length; i++)
            {
                Destroy(colls[i].gameObject);
            }

            StartCoroutine(Die());
            
        }
    }

    public IEnumerator Die()
    {
        AudioSource.PlayClipAtPoint(destroySound, transform.position);
        //explosionSound.Play();
        Instantiate(explosion, transform.position, transform.rotation);
        
        yield return new WaitForSeconds(0.00001f);
        gameObject.SetActive(false);
        GetComponent<Collider>().enabled = false;
    }
}