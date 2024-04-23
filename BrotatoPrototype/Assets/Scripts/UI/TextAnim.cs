using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class TextAnim : MonoBehaviour
{
    [SerializeField] private float typeSpeed = 0.1f;
   
    public void TypingText(TextMeshProUGUI textMeshProUGUI, string typeText, float startDelay)
    {
        StartCoroutine(TypeText(textMeshProUGUI, typeText, startDelay));
    }

    IEnumerator TypeText(TextMeshProUGUI textMeshProUGUI, string textStr, float startDelay)
    {
        yield return new WaitForSeconds(startDelay);
        textMeshProUGUI.text = string.Empty;
        for (int i = 0; i < textStr.Length; i++)
        {
            textMeshProUGUI.text += textStr[i];
            yield return new WaitForSeconds(typeSpeed);
        }

        yield return null;
    }
  
}
