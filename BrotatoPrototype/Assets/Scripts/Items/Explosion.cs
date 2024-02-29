using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Explosion : MonoBehaviour
{
    public bool isActive;
    [SerializeField] float radius;
    [SerializeField] float dmg;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform effectsTransform;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] AudioSource audioSource;


    private void Update()
    {
    //    if (isActive)
    //    {
    //        Explode();
    //        isActive = false;
    //    }
    }
    public void Explode()
    {
        Collider[] numColliders = Physics.OverlapSphere(transform.position, radius, layerMask);

        if (numColliders.Length > 0)
        {
            for (int i = 0; i < numColliders.Length; i++)
            {
                numColliders[i].GetComponent<EnemyHealth>().TakeHit(dmg, false);

                if (numColliders[i].TryGetComponent(out IKnockbackable knockbackable))
                {
                    knockbackable.GetKnocked(new Vector3(0, 120f, 0));
                    //numColliders[i].GetComponent<NavMeshAgent>().enabled = false; ;
                    //rb.useGravity = true;
                    //rb.isKinematic = false;
                    //// rb.AddExplosionForce(2000f, transform.position, 10f, 10f);
                    //rb.AddForce(numColliders[i].transform.up * 2000f);
                    //Debug.Log("бабабабааббах")

                }
            }
        }
        

        DisplayExplosion();
       // audioSource.Play();
    }

    private void DisplayExplosion()
    {
        Instantiate(explosionEffect, effectsTransform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        Explode();
        Destroy(gameObject);
    }

}
