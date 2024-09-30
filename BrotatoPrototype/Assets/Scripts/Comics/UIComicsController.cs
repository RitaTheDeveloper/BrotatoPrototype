using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComicsController : MonoBehaviour
{
    public ComicsOfWaveScriptable comicsOfWaveScriptable;

    [SerializeField] private GameManager _gameManager;    
    [SerializeField] private GameObject _comicsObj;
    [SerializeField] private GameObject[] _comics;
    private UIManager _uiManager;
    private ComicsOfWave[] _comicsOfWaves;

    private Sprite[] _currentComics = null;
    private bool isShow = false;

    private void Start()
    {
        _comicsOfWaves = comicsOfWaveScriptable.comicsOfWaves;
    }

    public void ComicsCheck(UIManager uiManager)
    {
        _uiManager = uiManager;
        Debug.Log("check comics");
        var currentWave = _gameManager.WaveCounter;
        isShow = false;
        foreach (var go in _comicsOfWaves)
        {
            if (currentWave == go.numberOfWave && go.comicsSprites != null)
            {
                _currentComics = go.comicsSprites;
                isShow = true;
            }
        }
        ShowComics(isShow);
    }

    private void ShowComics(bool isShow)
    {
        if (isShow)
        {
            Debug.Log("show comics");
            ShowComic(0);            
        }
        else
        {
            _uiManager.OpenShop();
        }        
    }

    public void OnClickClose()
    {
        LeanTween.alpha(_comicsObj.GetComponent<RectTransform>(), 0f, 1f).setEase(LeanTweenType.easeInOutQuad);
        _comicsObj.SetActive(false);
        _uiManager.OpenShop();
    }

    public void OnClickNextComics()
    {
        ShowComic(1);
    }

    public void OnClickBack()
    {
        ShowComic(0);
    }

    private void ShowComic(int index)
    {
        Debug.Log("length " + _currentComics.Length);
        if (_currentComics.Length > 1)
        {
            _comics[0].GetComponent<UIForComic>().Init(false);
            //_comics[index].GetComponent<UIForComic>().Init(false);
        }
        else
        {
            _comics[0].GetComponent<UIForComic>().Init(true);
        }
        AllComicsOff();
        _comicsObj.SetActive(true);
        _comics[index].SetActive(true);
        LeanTween.alpha(_comicsObj.GetComponent<RectTransform>(), 0f, 0f).setEase(LeanTweenType.easeInOutQuad);
        LeanTween.alpha(_comicsObj.GetComponent<RectTransform>(), 1f, 1f).setEase(LeanTweenType.easeInOutQuad);
    }

    private void AllComicsOff()
    {
        _comicsObj.SetActive(false);

        foreach (var go in _comics)
        {
            go.SetActive(false);
        }
    }

}
