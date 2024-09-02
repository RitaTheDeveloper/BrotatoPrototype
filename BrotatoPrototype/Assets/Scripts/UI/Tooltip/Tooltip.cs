using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class Tooltip : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _image;
    [SerializeField] private Transform _panel;

    private Canvas _canvas;
    private Vector2 _size;
    RectTransform _rectTr;

    public Vector2 GetSize => _size;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();

        _rectTr = transform.GetChild(0).GetComponent<RectTransform>();

        _size = _rectTr.sizeDelta;
    }

    public void SetPosition(Vector2 pos)
    {
        _rectTr.localPosition = pos;
    }

    public void Enable()
    {
        _canvas.enabled = true;
    }

    public void Disable()
    {
        _canvas.enabled = false;
    }

    public void InitTooltip(string text, Sprite sprite)
    {
        _text.text = text;

        if(sprite != null)
            _image.sprite = sprite;
    }

    public void InitTooltip(GameObject prefabUI)
    {
        if(prefabUI != null)
           Instantiate(prefabUI, _panel);

    }
}
