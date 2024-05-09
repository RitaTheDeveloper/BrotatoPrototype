using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

enum PhraseType
{
    IDLE,
    IN,
    OUT,
}

public class ShopPhrasesController : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("Список уровней фраз")]
    public List<ShopPhrasesLevel> phrases;

    private Dictionary<int, ShopPhrasesLevel> phrasesDictionary = new Dictionary<int, ShopPhrasesLevel>();

    private int currentShopLevel;

    private AudioSource audioSource;

    [Header("Адио микшер")]
    [SerializeField] public AudioMixerGroup mixerGroup;

    [Header("Fade In/Out time")]
    [SerializeField] float fadeTime;

    [Header("Время для проигрывания idle")]
    [SerializeField] float idleTime = 45;

    [Header("Shop in delay")]
    [SerializeField] float shopInDelay = 1.5f;

    [Header("Shop out delay")]
    [SerializeField] float shopOutDelay = 0.15f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        for (int i = 0; i < phrases.Count; i++)
        {
            phrasesDictionary.Add(phrases[i].phrasesLevel, phrases[i]);
        }
        audioSource.outputAudioMixerGroup = mixerGroup;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnShopIn()
    {
        currentShopLevel = UIShop.instance.GetBabaYagaIndex();
        PlayIN();
        StartCoroutine(idleCorutine());
    }

    public void OnShopOut()
    {
        StopAllCoroutines();
        PlayOUT();
    }

    IEnumerator FadeInSource(PhraseType nextPhrase)
    {
        while (audioSource.volume > 0 && audioSource.isPlaying)
        {
            float volume = audioSource.volume - (1 / fadeTime) * Time.deltaTime;
            audioSource.volume = volume > 0 ? volume : 0;
            yield return new WaitForEndOfFrame();
        }
        currentShopLevel = UIShop.instance.GetBabaYagaIndex();
        if (phrasesDictionary.ContainsKey(currentShopLevel))
        {
            switch (nextPhrase)
            {
                case PhraseType.IN:
                    if (phrasesDictionary[currentShopLevel].shopInPhrases.Count > 0)
                    {
                        Sound next = phrasesDictionary[currentShopLevel].shopInPhrases[UnityEngine.Random.Range(0, phrasesDictionary[currentShopLevel].shopInPhrases.Count)];
                        yield return new WaitForSecondsRealtime(shopInDelay);
                        SetSound(next);
                    }
                    break;
                case PhraseType.OUT:
                    if (phrasesDictionary[currentShopLevel].shopOutPhrases.Count > 0)
                    {
                        Sound next = phrasesDictionary[currentShopLevel].shopOutPhrases[UnityEngine.Random.Range(0, phrasesDictionary[currentShopLevel].shopOutPhrases.Count)];
                        yield return new WaitForSecondsRealtime(shopOutDelay);
                        SetSound(next);
                    }
                    break;
                case PhraseType.IDLE:
                    if (phrasesDictionary[currentShopLevel].idlePhrases.Count > 0)
                    {
                        Sound next = phrasesDictionary[currentShopLevel].idlePhrases[UnityEngine.Random.Range(0, phrasesDictionary[currentShopLevel].idlePhrases.Count)];
                        SetSound(next);
                    }
                    break;
            }
        }
        StartCoroutine(FadeOutSource());
        yield break;
    }

    IEnumerator FadeOutSource()
    {
        while (audioSource.volume < 1)
        {
            float volume = audioSource.volume + (1 / fadeTime) * Time.deltaTime;
            audioSource.volume = volume < 1 ? volume : 1;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    IEnumerator idleCorutine()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(idleTime);
            PlayIDLE();
        }
    }
    

    public void PlayIDLE()
    {
        StartCoroutine(FadeInSource(PhraseType.IDLE));
    }

    public void PlayIN()
    {
        StartCoroutine(FadeInSource(PhraseType.IN));
    }

    public void PlayOUT()
    {
        StartCoroutine(FadeInSource(PhraseType.OUT));
    }

    private void SetSound(Sound sound)
    {
        audioSource.clip = sound.clip;
        audioSource.volume = sound.volume;
        audioSource.pitch = sound.pitch;
        audioSource.loop = sound.loop;
        audioSource.Play();
    }
}
