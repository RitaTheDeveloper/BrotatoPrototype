using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mine : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float dmg;
    [SerializeField] Explosion _explosionPrefab;
    [SerializeField] GameObject modelOfMine;
    private bool activated;

    private void Start()
    {
        activated = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!activated)
        {
            Debug.Log("boom!");
            _explosionPrefab.Explode(transform.position, radius, dmg);
            modelOfMine.SetActive(false);
            Destroy(gameObject, 1f);
            activated = true;
        }
        
    }
}
