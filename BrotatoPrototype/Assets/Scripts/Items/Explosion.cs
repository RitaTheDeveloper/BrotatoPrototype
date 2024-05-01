using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Explosion : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] DrawCircle drawCircleForRadiusDmg;
    [SerializeField] AudioSource audioSource;

    public void Explode(Vector3 _position, float _radius, float _dmg)
    {
        Collider[] numColliders = Physics.OverlapSphere(_position, _radius, layerMask);

        if (numColliders.Length > 0)
        {
            for (int i = 0; i < numColliders.Length; i++)
            {
                numColliders[i].GetComponent<EnemyHealth>().TakeHit(_dmg, false);

                //if (numColliders[i].TryGetComponent(out IKnockbackable knockbackable))
                //{
                //    knockbackable.GetKnockedUp(new Vector3(0, 400f, 0));
                //}
            }
        }
        

        DisplayExplosion(_radius);
       // audioSource.Play();
    }

    private void DisplayExplosion(float _radius)
    {
        if (explosionEffect)
        {
           var explosion = Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation);
            explosion.transform.localScale = explosion.transform.localScale * _radius;
        }
       
    }

    public void DrawRadiusOfAoE(float _radius)
    {
        drawCircleForRadiusDmg.DrawTheCircle(_radius);
    }

}
