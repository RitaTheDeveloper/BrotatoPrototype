using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PopUpWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject panel;
    private GameObject myPanel;
    public void OnPointerEnter(PointerEventData eventData)
    {
        var myPanel = Instantiate(panel, transform);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (myPanel)
        {
            Destroy(myPanel);
        }
    }
}
