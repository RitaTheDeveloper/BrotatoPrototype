using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenCloseWindow
{
    private static float time = 0.25f;
    public Image img;
    public static void OpenWindow(GameObject window)
    {
        LeanTween.scale(window, Vector2.one, time).setEaseInCirc();        
    }

    public static void CloseWindow(GameObject window)
    {
        LeanTween.scale(window, Vector2.zero, time).setEaseOutCirc();
    }
    
    public static void OpenWindowWithDelay(GameObject window, float delay)
    {
        LeanTween.scale(window, Vector2.one, 0.5f).setEaseInCirc().setDelay(delay);
    }
}
