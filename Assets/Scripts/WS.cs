using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Websocket通信を実現のため
using WebSocketSharp;
using WebSocketSharp.Net;

//Json整形のため
using Newtonsoft.Json;

public class WS : MonoBehaviour {

	/**
	 * websocket通信サーバ情報
	 */
	WebSocket ws;

	public List<Person> obj = new List<Person>{
		new Person {id = "defalt id", state = "defalt state"}
	};

	// Use this for initialization
	void Start () {
		ws = new WebSocket("ws://localhost:3000/");

		GameObject poiObject = GameObject.Find ("Poi_kari");
		Poi GetPoi;
		GetPoi = poiObject.GetComponent<Poi>();
		//イベントハンドラの登録
		ws.OnOpen += (sender, e) =>
		{
			Debug.Log("WebSocket Open");
		};

		ws.OnMessage += (sender, e) =>
		{
			obj = JsonConvert.DeserializeObject<List<Person>>(e.Data);
			GetPoi.Translate(obj);
			//Debug.Log("WebSocket Message Type: " + e.GetType() + "\nData: " + e.Data);
		};

		ws.OnError += (sender, e) =>
		{
			Debug.Log("WebSocket Error Message: " + e.Message);
		};

		ws.OnClose += (sender, e) =>
		{
			Debug.Log("WebSocket Close");
		};

		ws.Connect();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("s")) {
			ws.Send("Test Message");
		}
	}



	/**
	 * <ol>
	 *   <li>ポイがデストロイされた場合の処理</li>
	 * </ol>
	 */
	void OnDestroy()
	{
		ws.Close();
		ws = null;
	}
}


