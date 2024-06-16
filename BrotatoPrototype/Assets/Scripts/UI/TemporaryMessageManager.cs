using System.Collections.Generic;
using UnityEngine;

public class TemporaryMessageManager : MonoBehaviour
{
    [SerializeField] protected int _textSize = 20;
    [SerializeField] protected Color _textColor = Color.red;
    [SerializeField] protected float _messageTime = 1;
    [Space(5)]
    [SerializeField] private int _countTextPool = 30;
    [SerializeField] private TextDamage _damageTextPrefab;
    [SerializeField] private Transform _poolPosition;

    private Transform _currentTransform;
    private Camera _camera;

    private Queue<TextDamage> _queueText = new();

    public static TemporaryMessageManager Instance { get; private set; }

    private void Awake()
    {
        _currentTransform = transform;
        _camera = Camera.main;
        Instance = this;

        for (int i = 0; i < _countTextPool; i++)
        {
            TextDamage newText = Instantiate(_damageTextPrefab, _currentTransform);
            newText.InitText(_poolPosition.position, this, _camera);
            AddQueue(newText);
        }
    }

    public void AddQueue(TextDamage textDamage)
    {
        _queueText.Enqueue(textDamage);
    }

    public void AddMessageOnScreen(string message, Vector3 position, Color color, float life_time = 1, int text_size = 20)
    {
        if (_queueText.Count > 0)
        {
            TextDamage text = _queueText.Dequeue();
            text.SetSettings(color, text_size, message, position + Vector3.up + Vector3.right, _messageTime);
            text.StartAnim();
        }
    }
}
