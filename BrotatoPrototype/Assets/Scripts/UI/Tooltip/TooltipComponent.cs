using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Tooltip _prefabTooltip;
    [SerializeField] private string _tooltipText;
    [SerializeField] private Sprite _sprite = null;
    [SerializeField] private GameObject uiPrefab = null;

    private Tooltip _tooltip;
    private Vector2 _sizeCanvas;
    private RectTransform _rectTransform;

    private bool _isWork = false;
    private Vector3 _mousePosition;

    private void Awake()
    {
        _tooltip = Instantiate(_prefabTooltip, transform);
        _tooltip.InitTooltip(_tooltipText, _sprite);
        _tooltip.InitTooltip(uiPrefab);
        _tooltip.Disable();

        GameObject obj = transform.parent.gameObject;

        while (true)
        {
            if (obj != null && obj.transform.parent != null)
            {
                if (obj.TryGetComponent(out Canvas canvas))
                {
                    _rectTransform = canvas.gameObject.GetComponent<RectTransform>();
                    _sizeCanvas = _rectTransform.sizeDelta * _rectTransform.localScale;

                    break;
                }
                else
                {
                    obj = obj.transform.parent.gameObject;
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isWork = true;
        _tooltip.InitTooltip(_tooltipText, _sprite);
        _tooltip.InitTooltip(uiPrefab);
        _tooltip.Enable();
        StartCoroutine(ShowTooltip());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isWork = false;
        _tooltip.Disable();
    }

    private IEnumerator ShowTooltip()
    {
        while (_isWork)
        {
            _mousePosition = Input.mousePosition;
            _sizeCanvas = _rectTransform.sizeDelta * _rectTransform.localScale;

            CheckInWindow();
            _tooltip.gameObject.transform.position = _mousePosition;

            yield return null;
        }
    }

    private void CheckInWindow()
    {
        Vector2 newPos = new Vector2(_mousePosition.x + _tooltip.GetSize.x * _rectTransform.localScale.x < _sizeCanvas.x ? 0 : -_tooltip.GetSize.x, _mousePosition.y + _tooltip.GetSize.y * _rectTransform.localScale.y < _sizeCanvas.y ? 0 : -_tooltip.GetSize.y);
        _tooltip.SetPosition(newPos);
    }

    public void SetText(string s)
    {
        _tooltipText = s;
    }

    public void SetUIPrefab(GameObject prefab)
    {
        uiPrefab = prefab;
    }
}
