using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIForComic : MonoBehaviour
{
    public Image comicImg;
    public Button closeBtn;
    public Button nextComicBtn;

    public void Init(bool isLast)
    {
        Debug.Log("comic is last " + isLast);
        if(closeBtn) closeBtn.gameObject.SetActive(isLast);
        if(nextComicBtn) nextComicBtn.gameObject.SetActive(!isLast);
    }
}
