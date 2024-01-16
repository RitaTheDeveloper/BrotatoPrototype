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
    public Vector3 Message_Position;

    public void MoveText(Camera camera)
    {
        float delta = 1.0f - (Message_Timer / Message_Life_Time);
        Vector3 position = Message_Position + new Vector3(delta, delta, 0.0f);
        position = camera.WorldToScreenPoint(position);
        position.z = 0.0f;

        Message.transform.position = position;
    }
}

public class TemporaryMessageManager : MonoBehaviour
{
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
                message.Message.gameObject.SetActive(false);
                node = node.Previous;
                Storage_Message.Remove(message);
            }
            else
            {
                var color = message.Message.color;
                color.a = message.Message_Timer / message.Message_Life_Time;
                message.Message.color = color;

                message.MoveText(Current_Cumera);
            }
            if (node.Next != null)
            {
                node = node.Next;
            }
            else
            {
                break;
            }
        }
    }

    public void AddMessageOnScreen(string message, Vector3 position)
    {
        TextMeshProUGUI message_ui = Instantiate(Text_Prefab, Current_Transform);
        message_ui.gameObject.SetActive(true);
        message_ui.text = message;

        TemporaryMessageConfig message_config = new TemporaryMessageConfig();
        message_config.Message_Life_Time = 1.0f;
        message_config.Message_Timer = 1.0f;
        message_config.Message = message_ui;
        message_config.Message_Position = position + Vector3.up;

        message_config.MoveText(Current_Cumera);
        Storage_Message.AddLast(message_config);
    }
}
