using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextDamage : MonoBehaviour
{
    private TMP_Text _text;
    private TemporaryMessageManager _manager;
    private Camera _camera;

    private Vector3 _pos;

    private float _timer;
    private float _time;

    public void InitText(TemporaryMessageManager manager, Camera camera)
    {
        _text = GetComponent<TMP_Text>();

        _manager = manager;
        _camera = camera;

        _text.gameObject.SetActive(false);

        _pos = transform.position;
    }

    public void SetSettings(Color color, int sizeText, string text, Vector3 startPosition, float lifeTime)
    {
        _timer = lifeTime;

        _text.color = color;
        _text.fontSize = sizeText;
        _text.text = text;

        transform.position = _camera.WorldToScreenPoint(startPosition);
        _pos = startPosition;
    }

    public void StartAnim()
    {
        _text.gameObject.SetActive(true);

        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        Color color;
        float delta;
        Vector3 pos;

        while (true)
        {
            if (_time >= _timer)
                break;

            color = _text.color;
            color.a = _timer > _time ? _timer - _time : 0;
            _text.color = color;

            delta = _timer - (_time / _timer);
            pos = _pos - new Vector3(delta, delta, 0);
            Debug.Log(pos);

            _text.transform.position = _camera.WorldToScreenPoint(pos);

            yield return null;

            _time += Time.deltaTime;
        }

        _time = 0;
        _text.gameObject.SetActive(false);
        _manager.AddQueue(this);
    }
}
