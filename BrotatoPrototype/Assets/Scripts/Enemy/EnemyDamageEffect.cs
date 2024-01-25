using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDamageEffect : MonoBehaviour
{
    [SerializeField] protected Material damageMaterial = null;
    [SerializeField] protected float damageTime = 1.0f;
    [SerializeField] protected float startAlpha = 128.0f;

    private float damageTimer = 0.0f;
    private float currentAlpha = 0.0f;
    private int materialIndex = -1;

    public void DoDamageEffect()
    {
        damageTimer = damageTime;
        currentAlpha = startAlpha;
        damageMaterial.color = new Color(damageMaterial.color.r, damageMaterial.color.g, damageMaterial.color.b, startAlpha);
        materialIndex = gameObject.GetComponent<Renderer>().materials.Length;
        gameObject.GetComponent<Renderer>().materials.Append<Material>(damageMaterial);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (damageTimer > 0.0f)
        {
            damageTimer -= Time.deltaTime;
            currentAlpha = startAlpha * (damageTimer / damageTime);
            damageMaterial.color = new Color(damageMaterial.color.r, damageMaterial.color.g, damageMaterial.color.b, currentAlpha);
        }
        else
        {
            if (gameObject.GetComponent<Renderer>().materials[materialIndex] != null)
            {
                Destroy(gameObject.GetComponent<Renderer>().materials[materialIndex]);
            }
        }
    }

    private void Awake()
    {

    }
}
