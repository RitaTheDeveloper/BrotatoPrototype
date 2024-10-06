using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private float amountOfHp;
    [SerializeField] private GameObject _vfxEffect;
    private GameObject _projectile;
    private Vector3 _position;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("хватай хилку");
            other.GetComponent<LivingEntity>().AddHealth(amountOfHp);
            _position = gameObject.transform.position;
            _position.y = _position.y + 2.1f;
            _projectile = Instantiate(_vfxEffect, _position, gameObject.transform.rotation);
            Destroy(gameObject);
            PlaySoundTakeHealing();
        }
    }

    private void PlaySoundTakeHealing()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("TakeHealing");
        }
    }
}
