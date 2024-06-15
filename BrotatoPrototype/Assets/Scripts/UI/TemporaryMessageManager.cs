using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TemporaryMessageConfig
{
    public TMP_Text Message;
    public float Message_Life_Time;
    public float Message_Timer;
    public float Message_Time;
    public Vector3 Message_Position;

    public void MoveText(Camera camera)
    {
        float delta = Message_Time - (Message_Timer / Message_Life_Time);
        Vector3 position = Message_Position + new Vector3(delta, delta, 0.0f);
        position = camera.WorldToScreenPoint(position);
        //position.z = 0.0f;

        Message.transform.position = position;
    }
}

public class TemporaryMessageManager : MonoBehaviour
{
    [SerializeField] protected int _textSize = 20;
    [SerializeField] protected Color _textColor = Color.red;
    [SerializeField] protected float _messageTime = 1;
    [SerializeField] private float _speedAnim = 1f;

    [Space(5)]
    [SerializeField] private int _countTextPool = 30;
    [SerializeField] private TextDamage _damageTextPrefab;
    [SerializeField] private Transform _poolPosition;

    private Transform _currentTransform;
    private Camera _camera;

    LinkedList<TemporaryMessageConfig> Storage_Message = new LinkedList<TemporaryMessageConfig>();






    private Queue<TextDamage> _queueText = new();







    public static TemporaryMessageManager Instance { get; private set; }

    public TextMeshProUGUI Text_Prefab;
    

    private void Awake()
    {
        _currentTransform = transform;
        _camera = Camera.main;
        Instance = this;

        for(int i = 0; i < _countTextPool; i++)
        {
            TextDamage newText = Instantiate(_damageTextPrefab, _currentTransform);
            newText.InitText(_speedAnim, _poolPosition.position, this, _camera);
            AddQueue(newText);
        }
    }

    //void Start()
    //{
    //    _camera = Camera.main;
    //    _currentTransform = transform;
    //}

    //void Update()
    //{
    //    var node = Storage_Message.First;
    //    while (node != null)
    //    {
    //        var message = node.Value;
    //        message.Message_Timer -= Time.deltaTime;

    //        if (message.Message_Timer <= 0.0f)
    //        {
    //            if (node.List.Count != 0 && node.Previous != null)
    //            {
    //                node = node.Previous;
    //            }
    //            message.Message.gameObject.SetActive(false);
    //            Storage_Message.Remove(message);
    //            Destroy(message.Message.gameObject, 0.0f);
    //        }
    //        else
    //        {
    //            var color = message.Message.color;
    //            color.a = message.Message_Timer / message.Message_Life_Time;
    //            message.Message.color = color;

    //            message.MoveText(_camera);
    //        }
    //        if (node.Next == null)
    //        {
    //            break;
    //        }
    //        else
    //        {
    //            node = node.Next;
    //        }
    //    }
    //}

    public void AddQueue(TextDamage textDamage)
    {
        _queueText.Enqueue(textDamage);
    }

    public void AddMessageOnScreen(string message, Vector3 position, Color color, float life_time = 1, int text_size = 20)
    {
        if(_queueText.Count > 0)
        {
            TextDamage text = _queueText.Dequeue();
            text.SetSettings(color, text_size, message, position + Vector3.up + Vector3.right, life_time);
            text.StartAnim();
        }




        //TextMeshProUGUI message_ui = Instantiate(Text_Prefab, _currentTransform);
        //message_ui.color = color;
        //message_ui.fontSize = text_size;
        //message_ui.gameObject.SetActive(true);
        ////message_ui.text = "-" + message;
        //message_ui.text = message;

        //TemporaryMessageConfig message_config = new TemporaryMessageConfig();
        //message_config.Message_Life_Time = life_time;
        //message_config.Message_Timer = 1.0f;
        //message_config.Message = message_ui;
        //message_config.Message_Position = position + (Vector3.up + Vector3.right);
        //message_config.Message_Time = message_time;

        //message_config.MoveText(_camera);

        //Storage_Message.AddLast(message_config);
    }
}
