using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseWindow
{
    private static float time = 0.25f;

    public static void OpenWindow(GameObject window)
    {
        LeanTween.scale(window, Vector2.one, time).setEaseInCirc();
        
    }

    public static void CloseWindow(GameObject window)
    {
        LeanTween.scale(window, Vector2.zero, time).setEaseOutCirc();
    }

}
