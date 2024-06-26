using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Tooltip _prefabTooltip;
    [SerializeField] private string _tooltipText;
    [SerializeField] private Sprite _sprite = null;
    [SerializeField] private float _timeShow = 1f;

    private Tooltip _tooltip;

    private Vector3 _mousePosition;

    private void Awake()
    {
        _tooltip = Instantiate(_prefabTooltip, transform);
        _tooltip.InitTooltip(_tooltipText, _sprite);
        _tooltip.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(ShowTooltip(_timeShow));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tooltip.gameObject.SetActive(false);
    }

    private IEnumerator ShowTooltip(float time)
    {
        yield return new WaitForSeconds(time);

        _mousePosition = Input.mousePosition;

        _tooltip.transform.position = _mousePosition;
        _tooltip.gameObject.SetActive(true);
    }
}
