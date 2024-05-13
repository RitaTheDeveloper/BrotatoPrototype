using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField] private float amountOfHp;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("хватай хилку");
            other.GetComponent<LivingEntity>().AddHealth(amountOfHp);
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
