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
            if (backgroundMusic[indexBackgroundMusic].CanPlayNext())
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
        if (stateMusicManager == StateMusicManager.FightPercs)
        {
            if (backgroundMusic[indexBackgroundMusic].CanPlayNext())
            {
                if (backgroundMusic.Count > indexBackgroundMusic + 1)
                {
                    indexBackgroundMusic++;
                    PlayBackgroundMusicPerkState();
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
        HelpPlayBackgroundMusic();
        stateMusicManager = StateMusicManager.Fight;
        StartCoroutine(FadeOutBackgroundMusic());
    }

    private void PlayBackgroundMusicPerkState()
    {
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
        StartCoroutine(FadeOutMainMenuMusic());
    }

    public void PlayShopMusicFromFight()
    {
        StartCoroutine(FadeInBackgroundMusicToShopMusic());
    }

    public void PlayMainMenuMusicFromFight()
    {
        StartCoroutine(FadeInBackgroundMusicToMenuMusic());
    }

    public void PlayShopMusicFromMainMenuMusic()
    {
        StartCoroutine(FadeInMainMenuMusicToShopMusic());
    }

    public void PlayMainMenuMusicFromShopMusic()
    {
        StartCoroutine(FadeInShopMusicToMenuMusic());
    }

    public void PlayBackgroundMusicFromMainMenuMusic()
    {
        StartCoroutine(FadeInMainMenuMusicToFight());
    }

    public void ReloadManager()
    {
        stateMusicManager = StateMusicManager.Fight;
        indexBackgroundMusic = 0;
    }

    public void ChangeBackgroundMusicToPercs()
    {
        StartCoroutine(FadeInBackGroundToPerksState());
    }

    public void PlayBackgroundMusicPerkStateFromMainMenuMusic()
    {
        StartCoroutine(FadeInMainMenuMusicToPerkState());
    }

    public void PlayBackgroundMusicFromShop()
    {
        StartCoroutine(FadeInShopMusicToFight());
    }

    IEnumerator FadeInBackgroundMusicToShopMusic()
    {
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
