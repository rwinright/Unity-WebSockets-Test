using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

// Use plugin namespace
using HybridWebSocket;

public class WebSocketDemo : MonoBehaviour
{

  WebSocket websocket;
  [SerializeField]
  Text text;
  string receivedMessage;
  int frameCount = 0;
  void Start()
  {

    // Create WebSocket instance
    websocket = WebSocketFactory.CreateInstance("ws://localhost:9000/msgs");
    // Add OnOpen event listener
    websocket.OnOpen += () =>
    {
      Debug.Log("websocket connected!");
      Debug.Log("websocket state: " + websocket.GetState().ToString());
    };

    // Add OnMessage event listener
    websocket.OnMessage += (byte[] msg) =>
    {
      receivedMessage = Encoding.UTF8.GetString(msg);
      Debug.Log(receivedMessage);
    };

    // Add OnError event listener
    websocket.OnError += (string errMsg) =>
    {
      Debug.Log("websocket error: " + errMsg);
    };

    // Add OnClose event listener
    websocket.OnClose += (WebSocketCloseCode code) =>
    {
      Debug.Log("websocket closed with code: " + code.ToString());
    };

    // Connect to the server
    websocket.Connect();

  }

  // Update is called once per frame
  void Update()
  {
    //On connect, send message every 100 frames
    if (websocket.GetState() == WebSocketState.Open)
    {
      frameCount++;
      if (frameCount >= 100)	 //Send message every 100 frames
      {
        // websocket.Send(Encoding.UTF8.GetBytes("hello"));
		SendMessages();
        frameCount = 0;
      }

      if (text != null)
        text.text = receivedMessage;
    }
    else
    {
      Debug.Log("websocket state: " + websocket.GetState().ToString());
    }
  }

  //On destroy, close connection
  void OnDestroy()
  {
    websocket.Close();
  }

  void SendMessages()
  {
    websocket.Send(Encoding.UTF8.GetBytes("hello"));
  }
}