using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostEffectController : MonoBehaviour
{

    public Shader postEffectDamagaShader;
    Material postEffectDamageMaterial;
    public float radius;
    public float feather;
    public Color tintColor;
    public float effectTime;

    private float effect_timer;
    private float start_radious = 1.3f;
    private float current_radius;

    private bool activate = false;

    // Start is called before the first frame update
    void Start()
    {
        current_radius = start_radious;
        effect_timer = effectTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (activate)
        {
            if (effect_timer > effectTime / 2) {
                current_radius -= (start_radious - radius) / (effectTime / 2 / Time.deltaTime);
            }
            else
            {
                current_radius += (start_radious - radius) / (effectTime / 2 / Time.deltaTime);
            }
            effect_timer -= Time.deltaTime;
            if (effect_timer <= 0)
            {
                activate = false;
                current_radius = start_radious;
            }
        }
    }

    private void Awake()
    {

    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (postEffectDamageMaterial == null)
        {
            postEffectDamageMaterial = new Material(postEffectDamagaShader);
        }

        int widht = source.width;
        int height = source.height;

        RenderTexture startRenderTexture = RenderTexture.GetTemporary(widht, height);


        postEffectDamageMaterial.SetFloat("_Radius", current_radius);
        postEffectDamageMaterial.SetFloat("_Feather", feather);
        postEffectDamageMaterial.SetColor("_TintColor", tintColor);

        Graphics.Blit(source, startRenderTexture, postEffectDamageMaterial);
        Graphics.Blit(startRenderTexture, destination);

        RenderTexture.ReleaseTemporary(startRenderTexture);
    }

    public void PlayDammageEffect()
    {
        if (!activate)
        {
            effect_timer = effectTime;
            activate = true;
        }
    }
}
