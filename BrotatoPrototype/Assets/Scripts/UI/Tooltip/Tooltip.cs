using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _image;

    public void InitTooltip(string text, Sprite sprite)
    {
        _text.text = text;

        if(sprite != null)
            _image.sprite = sprite;
    }
}
