using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class TestSocketClient : MonoBehaviour
{
    private SocketClient socketClient = new SocketClient();

    public string hostname;
    public int port;
    public NoldusSimpleBindings Bindings = new NoldusSimpleBindings();
    
    [Serializable]
    public class NoldusSimpleBindings
    {
        public SimpleEmotionMixerAsset Neutral;
        public SimpleEmotionMixerAsset Happy;
        public SimpleEmotionMixerAsset Sad;
        public SimpleEmotionMixerAsset Angry;
        public SimpleEmotionMixerAsset Surprised;
        public SimpleEmotionMixerAsset Scared;
        public SimpleEmotionMixerAsset Disgusted;
        public SimpleEmotionMixerAsset Valence;
        public SimpleEmotionMixerAsset Arousal;
        public SimpleEmotionMixerAsset Quality;

        public void SetValues(FaceReaderMessage msg)
        {
            //if (Mathf.Approximately(msg.Quality, 0f)) return;
            var quality = msg.Quality;
            var timeSinceStart = 0f;
            
            Neutral?.SetValue(msg.Neutral, quality, timeSinceStart);
            Happy?.SetValue(msg.Happy, quality, timeSinceStart);
            Sad?.SetValue(msg.Sad, quality, timeSinceStart);
            Angry?.SetValue(msg.Angry, quality, timeSinceStart);
            Surprised?.SetValue(msg.Surprised, quality, timeSinceStart);
            Scared?.SetValue(msg.Scared, quality, timeSinceStart);
            Disgusted?.SetValue(msg.Disgusted, quality, timeSinceStart);
            Valence?.SetValue(msg.Valence, quality, timeSinceStart);
            Arousal?.SetValue(msg.Arousal, quality, timeSinceStart);
        }
    }
    
    void OnDisable()
    {
        DisconnectFromSocketServer();
    }

    void OnEnable()
    {
        ConnectToSocketServer();
    }

    [ContextMenu("Disconnect from Socket Server")]
    public void DisconnectFromSocketServer()
    {
        socketClient.DisconnectFromTcpServer();
        socketClient.OnReceiveMessage -= OnReceiveMessage;

    }


    [ContextMenu("Connect to Socket Server")]
    public void ConnectToSocketServer()
    {
        try
        {
            socketClient.ConnectToTcpServer(hostname, port);
            socketClient.OnReceiveMessage += OnReceiveMessage;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Connecting to server {hostname}:{port} failed!");
            throw;
        }
    }

    private void OnReceiveMessage(string message)
    {
        Debug.Log("server message received as: " + message);
        try
        {
            FaceReaderMessage msg = FaceReaderMessage.FromJson(message);
            Bindings.SetValues(msg);
        }
        catch (ArgumentException e)
        {
            Debug.LogWarning($"Message is not a valid JSON:\n{message}");
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            socketClient.SendMessage("This is a message sent from Unity client");
        }
    }
}