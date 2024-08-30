using UnityEngine;
using UnityEngine.EventSystems;

public class UIInfoBoxReward : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UIInfoLevelReward _uIInfoLevelReward;

    public void Init(UIInfoLevelReward uIInfoLevelReward)
    {
        _uIInfoLevelReward = uIInfoLevelReward;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}

