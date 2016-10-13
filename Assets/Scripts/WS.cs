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

	GameObject poiObject;
	Poi GetPoi;

	private int connectionState = 0;
	private float timeleft;

	public List<Person> obj = new List<Person>{
		new Person {id = "defalt id", state = "defalt state"}
	};

	// Use this for initialization
	void Start () {
		//ws = new WebSocket("ws://localhost:3000/");
		poiObject = GameObject.Find ("Poi_kari");
		GetPoi = poiObject.GetComponent<Poi>();
		Connect ();

	}

	// Update is called once per frame
	void Update () {
		if(connectionState == 2){
			ws.Send ("Test Message");
		}
		timeleft -= Time.deltaTime;
		if (timeleft <= 0.0) {
			timeleft = 1.0f;

			if (connectionState == 0) {
				Connect ();
			}
		}


	}


	/// <summary>
	/// サーバーへ接続する
	/// </summary>
	private void Connect()
	{

		System.Threading.Thread.Sleep(1000);
		ws = new WebSocket ("ws://163.221.68.229:8080");

		//イベントハンドラの登録
		ws.OnOpen += (sender, e) =>
		{
			connectionState = 2;
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
			//ws = new WebSocket ("ws://163.221.68.229:8080");
			//ws.Close();
			connectionState = 0;
		};

		ws.OnClose += (sender, e) =>
		{
			connectionState = 0;
			Debug.Log("WebSocket Close");
		};


		connectionState = 1;
		/// サーバー接続開始
		ws.Connect();

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


