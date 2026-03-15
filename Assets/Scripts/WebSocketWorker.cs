using UnityEngine;
using WebSocketSharp;
using System;
using System.Collections.Generic;
using TMPro;

public class WebSocketClient : MonoBehaviour
{
    private WebSocket ws;

    private Queue<string> messageQueue = new Queue<string>();

    // temp
    public TextMeshProUGUI transcriptionText;
    public bool sendText = false;

    [Serializable]
    public class TextData
    {
        public string type;
        public string text;
    }

    void Start()
    {
        ws = new WebSocket("ws://localhost:8000/ws");

        ws.OnOpen += (sender, e) =>
        {
            Debug.Log("Connected");
        };

        ws.OnMessage += (sender, e) =>
        {
            lock (messageQueue)
            {
                messageQueue.Enqueue(e.Data);
            }
        };

        ws.Connect();

            // Check the number of available microphone devices
            if (Microphone.devices.Length > 0)
            {
                Debug.Log("Microphone detected. Available devices:");
                // Iterate through and print the names of all detected microphones
                foreach (string device in Microphone.devices)
                {
                    Debug.Log("- " + device);
                }
            }
            else
            {
                Debug.LogWarning("No microphone detected on this system.");
            }
    }

    void Update()
    {
        // TEMP
        if (sendText)
        {
            SendText(transcriptionText.text);
            Debug.Log("Send");
            sendText = false;
        }

        lock (messageQueue)
        {
            while (messageQueue.Count > 0)
            {
                string msg = messageQueue.Dequeue();
                HandleServerMessage(msg);
            }
        }
    }

    void HandleServerMessage(string json)
    {
        Debug.Log("Processing message on main thread: " + json);

        // Safe to modify Unity objects here
        // Example:
        // someGameObject.transform.position = new Vector3(...);
    }

    void OnApplicationQuit()
    {
        ws.Close();
    }

    public void SendText(string voiceManagerText)
    {
        TextData data = new TextData
        {
            type = "text_message",
            text = voiceManagerText
        };

        ws.Send(JsonUtility.ToJson(data));
    }
}