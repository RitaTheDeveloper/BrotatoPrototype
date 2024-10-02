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
    private bool _isShow = false;
    int _currentWave;

    private void Start()
    {
        _comicsOfWaves = comicsOfWaveScriptable.comicsOfWaves;
    }

    public void ComicsCheck(UIManager uiManager)
    {
        _uiManager = uiManager;
        Debug.Log("check comics");
        _currentWave = _gameManager.WaveCounter;
        _isShow = false;
        foreach (var go in _comicsOfWaves)
        {
            if (_currentWave == go.numberOfWave && go.comicsSprites.Length > 0)
            {
                _currentComics = go.comicsSprites;
                _isShow = true;
            }
        }
        ShowComics(_isShow);
    }

    private void ShowComics(bool isShow)
    {
        if (isShow)
        {
            Debug.Log("show comics");

            SetImages();
            ShowComic(0);            
        }
        else
        {
            _uiManager.OpenShop();
        }        
    }

    public void OnClickClose()
    {
        PlaySoundOfButtonPress();
        LeanTween.alpha(_comicsObj.GetComponent<RectTransform>(), 0f, 1f).setEase(LeanTweenType.easeInOutQuad);
        _comicsObj.SetActive(false);
        if (_currentWave == _comicsOfWaves.Length)
        {
            _uiManager.OnClickMenu();
        }
        else
        {
            _uiManager.OpenShop();
        }       
    }

    public void OnClickNextComics()
    {
        PlaySoundOfButtonPress();
        ShowComic(1);
    }

    public void OnClickBack()
    {
        PlaySoundOfButtonPress();
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

    private void PlaySoundOfButtonPress()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.Play("ClickElement");
        }
    }

    private void SetImages()
    {
        for (int i = 0; i < _currentComics.Length; i++)
        {
            _comics[i].GetComponent<UIForComic>().SetImage(_currentComics[i]);
        }
    }

}
