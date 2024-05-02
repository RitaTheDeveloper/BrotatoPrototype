using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum StateMusicManager
{
    Fight,
    FightPercs,
    ShopMenu,
    MainMenu,
    FadeIn, 
}

public class BackgroundMusicManger : MonoBehaviour
{
    public static BackgroundMusicManger instance;

    [Tooltip("List background music to play")]
    [SerializeField] public List<BackGroundMusic> backgroundMusic;

    [Tooltip("List shop music to play")]
    [SerializeField] public List<Sound> shopMusic;

    [Tooltip("List menu music to play")]
    [SerializeField] public List<Sound> menuMusic;

    [Tooltip("Bacckground Audio Mixer")]
    [SerializeField] public AudioMixerGroup backgroundMixer;

    [Header("Время FadeIn/FadeOut")]
    [SerializeField] public float fadeTime = 1.5f;

    public StateMusicManager stateMusicManager = StateMusicManager.MainMenu;

    public AudioSource backgroundSource;
    private AudioSource shopSource;
    private AudioSource mainMenuSource;

    private int indexBackgroundMusic = 0;

    private float stepVolume = 0;
    private bool canPlayBackground = false;

    private void Awake()
    {
        instance = this;
        backgroundSource = gameObject.AddComponent<AudioSource>();
        stepVolume = 1 / fadeTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayMainMenuSource();
    }

    // Update is called once per frame
    void Update()
    {

        if (stateMusicManager == StateMusicManager.MainMenu)
        {
            if (!mainMenuSource.isPlaying)
            {
                PlayMainMenuSource();
            }
        }
        if (stateMusicManager == StateMusicManager.ShopMenu)
        {
            if (!shopSource.isPlaying)
            {
                PlayShopMusic();
            }
        }
        if (stateMusicManager == StateMusicManager.Fight)
        {
            if (backgroundMusic[indexBackgroundMusic].CanPlayNext() && canPlayBackground)
            {
                if (backgroundMusic.Count > indexBackgroundMusic + 1)
                {
                    indexBackgroundMusic++;
                    PlayBackgroundMusic();
                }
                else
                {
                    backgroundSource.Stop();
                }
            }
        }
    }

    private void PlayShopMusic()
    {
        if (stateMusicManager == StateMusicManager.ShopMenu)
            if (shopSource != null)
                if (shopSource.isPlaying)
                    return;
        stateMusicManager = StateMusicManager.ShopMenu;
        if (mainMenuSource != null)
        {
            Destroy(mainMenuSource);
            mainMenuSource = null;
        }
        if (backgroundSource != null)
        {
            backgroundSource.Pause();
        }

        if (shopSource == null)
        {
            shopSource = gameObject.AddComponent<AudioSource>();
        }
        stateMusicManager = StateMusicManager.ShopMenu;
        if (shopMusic.Count == 0)
            return;
        int index = Random.Range(0, shopMusic.Count);
        shopSource.clip = shopMusic[index].clip;
        shopSource.volume = 0;
        shopSource.pitch = shopMusic[index].pitch;
        shopSource.loop = shopMusic[index].loop;
        shopSource.outputAudioMixerGroup = backgroundMixer;
        shopSource.Play();
        StopAllCoroutines();
        StartCoroutine(FadeOutShopMusic());
    }

    private void HelpPlayBackgroundMusic()
    {
        if (shopSource != null)
        {
            Destroy(shopSource);
            shopSource = null;
        }
        if (mainMenuSource != null)
        {
            Destroy(mainMenuSource);
            mainMenuSource = null;
        }

        if (backgroundSource == null)
        {
            backgroundSource = gameObject.AddComponent<AudioSource>();
        }
        backgroundSource.clip = backgroundMusic[indexBackgroundMusic].musicClip.clip;
        backgroundSource.volume = 0;
        backgroundSource.pitch = backgroundMusic[indexBackgroundMusic].musicClip.pitch;
        backgroundSource.loop = backgroundMusic[indexBackgroundMusic].musicClip.loop;
        backgroundSource.outputAudioMixerGroup = backgroundMixer;
        backgroundSource.Play();
    }

    private void PlayBackgroundMusic()
    {
        StopAllCoroutines();
        HelpPlayBackgroundMusic();
        stateMusicManager = StateMusicManager.Fight;
        StartCoroutine(FadeOutBackgroundMusic());
    }

    private void PlayBackgroundMusicPerkState()
    {
        StopAllCoroutines();
        HelpPlayBackgroundMusic();
        stateMusicManager = StateMusicManager.FightPercs;
        StartCoroutine(FadeOutBackgroundMusicToPerkState());
    }

    private void PlayMainMenuSource() {
        if (stateMusicManager == StateMusicManager.MainMenu)
            if (mainMenuSource != null)
                if (mainMenuSource.isPlaying)
                    return;
        if (shopSource != null)
        {
            Destroy(shopSource);
            shopSource = null;
        }
        if (backgroundSource != null)
        {
            backgroundSource.Pause();
        }

        if (mainMenuSource == null)
        {
            mainMenuSource = gameObject.AddComponent<AudioSource>();
        }
        stateMusicManager = StateMusicManager.MainMenu;
        if (menuMusic.Count == 0)
            return;
        int index = Random.Range(0, menuMusic.Count);
        mainMenuSource.clip = menuMusic[index].clip;
        mainMenuSource.volume = 0;
        mainMenuSource.pitch = menuMusic[index].pitch;
        mainMenuSource.loop = menuMusic[index].loop;
        mainMenuSource.outputAudioMixerGroup = backgroundMixer;
        mainMenuSource.Play();
        StopAllCoroutines();
        StartCoroutine(FadeOutMainMenuMusic());
    }

    public void PlayShopMusicFromFight()
    {
        canPlayBackground = false;
        StopAllCoroutines();
        stateMusicManager = StateMusicManager.FadeIn;
        StartCoroutine(FadeInBackgroundMusicToShopMusic());
    }

    public void PlayMainMenuMusicFromFight()
    {
        canPlayBackground = false;
        StopAllCoroutines();
        stateMusicManager = StateMusicManager.FadeIn;
        StartCoroutine(FadeInBackgroundMusicToMenuMusic());
    }

    public void PlayShopMusicFromMainMenuMusic()
    {
        canPlayBackground = false;
        StopAllCoroutines();
        stateMusicManager = StateMusicManager.FadeIn;
        StartCoroutine(FadeInMainMenuMusicToShopMusic());
    }

    public void PlayMainMenuMusicFromShopMusic()
    {
        canPlayBackground = false;
        StopAllCoroutines();
        stateMusicManager = StateMusicManager.FadeIn;
        StartCoroutine(FadeInShopMusicToMenuMusic());
    }

    public void PlayBackgroundMusicFromMainMenuMusic()
    {
        canPlayBackground = true;
        StopAllCoroutines();
        stateMusicManager = StateMusicManager.FadeIn;
        StartCoroutine(FadeInMainMenuMusicToFight());
    }

    public void ReloadManager()
    {
        stateMusicManager = StateMusicManager.Fight;
        indexBackgroundMusic = 0;
    }

    public void ChangeBackgroundMusicToPercs()
    {
        canPlayBackground = true;
        StopAllCoroutines();
        stateMusicManager = StateMusicManager.FadeIn;
        StartCoroutine(FadeInBackGroundToPerksState());
    }

    public void PlayBackgroundMusicPerkStateFromMainMenuMusic()
    {
        canPlayBackground = true;
        StopAllCoroutines();
        stateMusicManager = StateMusicManager.FadeIn;
        StartCoroutine(FadeInMainMenuMusicToPerkState());
    }

    public void PlayBackgroundMusicFromShop()
    {
        canPlayBackground= true;
        StopAllCoroutines();
        stateMusicManager = StateMusicManager.FadeIn;
        StartCoroutine(FadeInShopMusicToFight());
    }

    IEnumerator FadeInBackgroundMusicToShopMusic()
    {
        if (backgroundSource == null)
        {
            PlayShopMusic();
            yield break;
        }
        while (backgroundSource.volume > 0)
        {
            if (backgroundSource.volume - stepVolume * Time.deltaTime < 0) backgroundSource.volume = 0;
            else backgroundSource.volume -= stepVolume * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("OpenDoor");
        }
        PlayShopMusic();
        yield break;
    }

    IEnumerator FadeInBackgroundMusicToMenuMusic() {
        if (backgroundSource == null)
        {
            PlayMainMenuSource();
            yield break;
        }
        while (backgroundSource.volume > 0)
        {
            if (backgroundSource.volume - stepVolume * Time.deltaTime < 0) backgroundSource.volume = 0;
            else backgroundSource.volume -= stepVolume * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        PlayMainMenuSource();
        yield break;
    }

    IEnumerator FadeInShopMusicToFight()
    {
        if (shopSource == null)
        {
            PlayBackgroundMusic();
            yield break;
        }
        while (shopSource.volume > 0)
        {
            if (shopSource.volume - stepVolume * Time.deltaTime < 0) shopSource.volume = 0;
            else shopSource.volume -= stepVolume * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("CloseDoor");
        }
        PlayBackgroundMusic();
        yield break;
    }

    IEnumerator FadeInShopMusicToMenuMusic()
    {
        if (shopSource == null)
        {
            PlayMainMenuSource();
            yield break;
        }
        while (shopSource.volume > 0)
        {
            if (shopSource.volume - stepVolume * Time.deltaTime < 0) shopSource.volume = 0;
            else shopSource.volume -= stepVolume * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        PlayMainMenuSource();
        yield break;
    }

    IEnumerator FadeInMainMenuMusicToFight() {
        if (mainMenuSource == null)
        {
            PlayBackgroundMusic();
            yield break;
        }
        while (mainMenuSource.volume > 0)
        {
            if (mainMenuSource.volume - stepVolume * Time.deltaTime < 0) mainMenuSource.volume = 0;
            else mainMenuSource.volume -= stepVolume * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        PlayBackgroundMusic();
        yield break;
    }

    IEnumerator FadeInMainMenuMusicToShopMusic()
    {
        if (mainMenuSource == null)
        {
            PlayShopMusic();
            yield break;
        }
        while (mainMenuSource.volume > 0)
        {
            if (mainMenuSource.volume - stepVolume * Time.deltaTime < 0) mainMenuSource.volume = 0;
            else mainMenuSource.volume -= stepVolume * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        PlayShopMusic();
        yield break;
    }

    IEnumerator FadeInMainMenuMusicToPerkState()
    {
        if (mainMenuSource == null)
        {
            PlayBackgroundMusicPerkState();
            yield break;
        }
        while (mainMenuSource.volume > 0)
        {
            if (mainMenuSource.volume - stepVolume * Time.deltaTime < 0) mainMenuSource.volume = 0;
            else mainMenuSource.volume -= stepVolume * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        PlayBackgroundMusicPerkState();
        yield break;
    }

    IEnumerator FadeOutBackgroundMusic() 
    {
        if (backgroundSource == null)
        {
            yield break;
        }
        while (backgroundSource.volume < 1)
        {
            if (backgroundSource.volume + stepVolume * Time.deltaTime > 1) backgroundSource.volume = 1;
            else backgroundSource.volume += stepVolume * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    IEnumerator FadeOutShopMusic()
    {
        if (shopSource == null)
            yield break;
        while (shopSource.volume < 1)
        {
            if (shopSource.volume + stepVolume * Time.deltaTime > 1) shopSource.volume = 1;
            else shopSource.volume += stepVolume * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    IEnumerator FadeOutMainMenuMusic()
    {
        if (mainMenuSource == null)
            yield break;
        while (mainMenuSource.volume < 1)
        {
            if (mainMenuSource.volume + stepVolume * Time.deltaTime > 1) mainMenuSource.volume = 1;
            else mainMenuSource.volume += stepVolume * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    IEnumerator FadeInBackGroundToPerksState()
    {
        if (backgroundSource == null)
        {
            PlayBackgroundMusicPerkState();
            yield break;
        }
        while (backgroundSource.volume > 0.7f)
        {
            if (backgroundSource.volume - stepVolume * Time.deltaTime < 0.7f) backgroundSource.volume = 0.7f;
            else backgroundSource.volume -= stepVolume * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        stateMusicManager = StateMusicManager.FightPercs;
        yield break;
    }

    IEnumerator FadeOutBackgroundMusicToPerkState()
    {
        if (backgroundSource == null)
            yield break;
        while (backgroundSource.volume < 0.7f)
        {
            if (backgroundSource.volume + stepVolume * Time.deltaTime > 0.7f) backgroundSource.volume = 0.7f;
            else backgroundSource.volume += stepVolume * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        stateMusicManager = StateMusicManager.FightPercs;
        yield break;
    }
}
