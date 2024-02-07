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
    private bool isDamage = false;
    private Material[] oldMaterials = null;

    public void DoDamageEffect()
    {
        damageTimer = damageTime;
        currentAlpha = startAlpha;
        damageMaterial.color = new Color(damageMaterial.color.r, damageMaterial.color.g, damageMaterial.color.b, startAlpha);

        MeshRenderer mr = this.gameObject.GetComponentInChildren<MeshRenderer>();
        if (mr != null)
        {
            materialIndex = mr.materials.Length;
            oldMaterials = mr.materials;
            mr.materials = new[] { damageMaterial }.Concat(oldMaterials).ToArray();

        }
        isDamage = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDamage)
        {
            if (damageTimer > 0.0f)
            {
                damageTimer -= Time.deltaTime;
                currentAlpha = startAlpha * (damageTimer / damageTime);
                this.gameObject.GetComponentInChildren<MeshRenderer>().materials[0].color = new Color(damageMaterial.color.r, damageMaterial.color.g, damageMaterial.color.b, currentAlpha);
            }
            else
            {
                if (materialIndex > -1)
                {
                    MeshRenderer mr = this.gameObject.GetComponentInChildren<MeshRenderer>();
                    mr.materials = oldMaterials;
                }
            }
        }
    }

    private void Awake()
    {

    }
}
