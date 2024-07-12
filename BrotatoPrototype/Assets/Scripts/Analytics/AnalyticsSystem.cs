using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Analytics;
using UnityEngine.Analytics;
using System;

public class AnalyticsSystem : MonoBehaviour
{
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            GiveConsent(); //Get user consent according to various legislations
        }
        catch (ConsentCheckException e)
        {
            Debug.Log(e.ToString());
        }
    }

    public void GiveConsent()
    {
        // Call if consent has been given by the user
        AnalyticsService.Instance.StartDataCollection();
        Debug.Log($"Consent has been provided. The SDK is now collecting data!");
    }

    public void OnPlayerDead(int waveNumber, string nameHero)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            {"wave", waveNumber },
            {"hero", nameHero }
        };

        AnalyticsService.Instance.CustomData("playerDead", parameters);
        AnalyticsService.Instance.Flush();
    }

    public void WaveCompleted(int waveNumber, string nameHero)
    {
        Dictionary<string, object> parameters =  new Dictionary<string, object>()
        {
            {"wave", waveNumber },
            {"hero", nameHero }
        };

        AnalyticsService.Instance.CustomData("waveCompleted", parameters);
        AnalyticsService.Instance.Flush();
    }

    public void OnStartedPlaying(string nameHero)
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "hero", nameHero}
        };

        AnalyticsService.Instance.CustomData("startedPlaying", parameters);
        AnalyticsService.Instance.Flush();

    }

    public void OnStart()
    {
        Analytics.CustomEvent("startGame");
        AnalyticsService.Instance.RecordEvent("startGame");
        AnalyticsService.Instance.Flush();
    }

    public void OnGameOver()
    {
        Analytics.CustomEvent("gameOver");
    }

    public void OnClickedUpgradeShop()
    {

    }

}
