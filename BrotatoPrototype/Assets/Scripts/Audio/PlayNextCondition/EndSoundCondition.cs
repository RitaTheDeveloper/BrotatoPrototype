using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSoundCondition : ConditionClass
{
    public AudioSource audioSource;

    private void Awake()
    {
        if (BackgroundMusicManger.instance != null)
        {
            audioSource = BackgroundMusicManger.instance.backgroundSource;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public override bool CanPlayNext()
    {
        return !BackgroundMusicManger.instance.backgroundSource.isPlaying;
    }
}
