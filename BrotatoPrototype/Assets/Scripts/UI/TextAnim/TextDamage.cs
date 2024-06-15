using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextDamage : MonoBehaviour
{
    private TMP_Text _text;
    private TemporaryMessageManager _manager;
    private Camera _camera;

    private Vector3 _poolPosition;

    private float _timer;
    private float _time;

    private float _speed;

    public bool IsActive { get; private set; }

    public void InitText(float speed, Vector3 poolPosition, TemporaryMessageManager manager, Camera camera)
    {
        _text = GetComponent<TMP_Text>();

        _speed = speed;
        _poolPosition = poolPosition;
        _manager = manager;
        _camera = camera;

        _text.gameObject.SetActive(false);

        IsActive = false;
    }

    public void SetSettings(Color color, int sizeText, string text, Vector3 startPosition, float lifeTime)
    {
        _timer = lifeTime;

        _text.color = color;
        _text.fontSize = sizeText;
        _text.text = text;

        transform.position = _camera.WorldToScreenPoint(startPosition);
    }

    public void StartAnim()
    {
        IsActive = true;
        _text.gameObject.SetActive(true);

        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        while (true)
        {
            if (_time >= _timer)
                break;

            transform.position += _speed * Time.deltaTime * new Vector3(1, 1, 0);

            yield return null;

            _time += Time.deltaTime;
        }

        _time = 0;
        IsActive = false;
        _text.gameObject.SetActive(false);
        _manager.AddQueue(this);
    }
}
