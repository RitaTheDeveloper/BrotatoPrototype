using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsSystem : MonoBehaviour
{
    public void OnPlayerDead(int waveNumber, string nameHero)
    {
        Analytics.CustomEvent("Player Dead", new Dictionary<string, object>()
        {
            {"Wave",  waveNumber},
            {"Hero", nameHero}
        });
    }

    public void WaveCompleted(int waveNumber, string nameHero)
    {
        Analytics.CustomEvent("Wave Completed", new Dictionary<string, object>()
        {
            {"Wave", waveNumber },
            {"Hero", nameHero }
        });
    }

    public void OnStartedPlaying(string nameHero)
    {
        Analytics.CustomEvent("Started Playing", new Dictionary<string, object>()
        {
            { "Hero", nameHero}            
        });
    }
}
