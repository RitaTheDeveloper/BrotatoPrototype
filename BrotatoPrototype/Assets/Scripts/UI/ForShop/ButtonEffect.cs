using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private Image pot;
    [SerializeField] private Sprite spriteGlowPot;

    private Sprite spritePot;

    public void OnPointerDown(PointerEventData eventData)
    {
        pot.sprite = spritePot;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        pot.sprite = spritePot;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        spritePot = pot.sprite;
        pot.sprite = spriteGlowPot;
    }
}
