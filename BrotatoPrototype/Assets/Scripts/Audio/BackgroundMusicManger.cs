using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum StateMusicManager
{
    Fight,
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

    public StateMusicManager stateMusicManager = StateMusicManager.MainMenu;

    public AudioSource backgroundSource;
    private AudioSource shopSource;
    private AudioSource mainMenuSource;

    private int indexBackgroundMusic = 0;

    private void Awake()
    {
        instance = this;
        backgroundSource = gameObject.AddComponent<AudioSource>();
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
    }

    public void PlayBackgroundMusic()
    {
        if (shopSource != null) {
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
        backgroundSource.volume = backgroundMusic[indexBackgroundMusic].musicClip.volume;
        backgroundSource.pitch = backgroundMusic[indexBackgroundMusic].musicClip.pitch;
        backgroundSource.loop = backgroundMusic[indexBackgroundMusic].musicClip.loop;
        backgroundSource.outputAudioMixerGroup = backgroundMixer;
        backgroundSource.Play();
        stateMusicManager = StateMusicManager.Fight;
    }

    public void PlayMainMenuSource() {
        if (stateMusicManager == StateMusicManager.MainMenu)
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
        mainMenuSource.volume = menuMusic[index].volume;
        mainMenuSource.pitch = menuMusic[index].pitch;
        mainMenuSource.loop = menuMusic[index]  .loop;
        mainMenuSource.outputAudioMixerGroup = backgroundMixer;
        mainMenuSource.Play();
    }

    public void PlayShopMusic()
    {
        if (stateMusicManager == StateMusicManager.ShopMenu)
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
        shopSource.volume = shopMusic[index].volume;
        shopSource.pitch = shopMusic[index].pitch;
        shopSource.loop = shopMusic[index].loop;
        shopSource.outputAudioMixerGroup = backgroundMixer;
        shopSource.Play();
    }

    public void ReloadManager()
    {
        stateMusicManager = StateMusicManager.Fight;
        indexBackgroundMusic = 0;
    }
}
