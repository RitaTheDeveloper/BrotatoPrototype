using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TemporaryMessageConfig
{
    public TextMeshProUGUI Message;
    public float Message_Life_Time;
    public float Message_Timer;
    public float Message_Time;
    public Vector3 Message_Position;

    public void MoveText(Camera camera)
    {
        float delta = Message_Time - (Message_Timer / Message_Life_Time);
        Vector3 position = Message_Position + new Vector3(delta, delta, 0.0f);
        position = camera.WorldToScreenPoint(position);
        position.z = 0.0f;

        Message.transform.position = position;
    }
}

public class TemporaryMessageManager : MonoBehaviour
{
    [SerializeField] protected int text_size = 20;
    [SerializeField] protected Color text_color = Color.red;
    [SerializeField] protected float message_time = 1;

    public static TemporaryMessageManager Instance { get; private set; }

    public TextMeshProUGUI Text_Prefab;

    private void Awake()
    {
        Instance = this;
    }

    Camera Current_Cumera;
    Transform Current_Transform;
    LinkedList<TemporaryMessageConfig> Storage_Message = new LinkedList<TemporaryMessageConfig>();

    // Start is called before the first frame update
    void Start()
    {
        Current_Cumera = Camera.main;
        Current_Transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        var node = Storage_Message.First;
        while (node != null)
        {
            var message = node.Value;
            message.Message_Timer -= Time.deltaTime;

            if (message.Message_Timer <= 0.0f)
            {
                if (node.List.Count != 0 && node.Previous != null)
                {
                    node = node.Previous;
                }
                message.Message.gameObject.SetActive(false);
                Storage_Message.Remove(message);
                Destroy(message.Message.gameObject, 0.0f);
            }
            else
            {
                var color = message.Message.color;
                color.a = message.Message_Timer / message.Message_Life_Time;
                message.Message.color = color;

                message.MoveText(Current_Cumera);
            }
            if (node.Next == null)
            {
                break;
            }
            else
            {
                node = node.Next;
            }
        }
    }

    public void AddMessageOnScreen(string message, Vector3 position, Color color, float life_time = 1, int text_size = 20)
    {
        TextMeshProUGUI message_ui = Instantiate(Text_Prefab, Current_Transform);
        message_ui.color = color;
        message_ui.fontSize = text_size;
        message_ui.gameObject.SetActive(true);
        message_ui.text = "-" + message;

        TemporaryMessageConfig message_config = new TemporaryMessageConfig();
        message_config.Message_Life_Time = life_time;
        message_config.Message_Timer = 1.0f;
        message_config.Message = message_ui;
        message_config.Message_Position = position + (Vector3.up + Vector3.right);
        message_config.Message_Time = message_time;

        message_config.MoveText(Current_Cumera);

        Storage_Message.AddLast(message_config);
    }
}
