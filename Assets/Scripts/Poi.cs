//基本的なC#
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Websocket通信を実現のため
using WebSocketSharp;
using WebSocketSharp.Net;

//Json整形のため
using Newtonsoft.Json;

/**
 * ポイ情報管理クラス
 * 
 * @author 最終更新者 kazuhito-u
 */
public class Poi : Token {

	/**
	 * ユーザのポイント
	 */ 
	static int userPoint = 0;

	/**
	 * senstickの振られたかどうか(振られたらtrue)
	 */
	bool senstickStatus = false;

	/**
	 * ポイの名前
	 */
	public string myName;


	/**
	 * <ol>
	 *   <li>ポイクラスの初期化処理</li>
	 *   <li>websocket初期化処理</li>
	 *   <li>Camera初期化処理</li>
	 * </ol>
	 */
	void Start () {
		//ここでは今の所Websocket通信の初期化だけしてるけど
		//Cameraの初期化とかいると思うんで追記しておいてください


	}


	
	/**
	 * <ol>
	 *   <li>1フレームごとの処理</li>
	 * </ol>
	 */
	void Update () {
		//var obj = GetComponent<WS>.obj;

	}


	/**
	 * <ol>
	 *   <li>ポイがデストロイされた場合の処理</li>
	 * </ol>
	 */
	void OnDestroy()
	{

	}


	public void Translate(List<Person> e)
	{
		
		foreach(var item in e){
			Debug.Log("fujiwara ," + myName);
			Debug.Log("Name:" + item.id + ", state:" + item.state + ", myName" + myName);
			if(myName == item.id && item.state == "1"){
				Debug.Log("同じ名前と結果やんけ！"+ myName);
				senstickStatus = true;
			}
			else if(myName == item.id && item.state=="0"){
				Debug.Log("違うのかよ!");
				senstickStatus = false;
			}
		}

	}



	/**
	 * <ol>
	 *   <li>ユーザのポイントを加点するかどうかの判定</li>
	 * </ol>
	 */
	void getpoint(){
		//加点するポイントが5点固定だとした場合
		userPoint += 5;
	}

	/**
	 * <ol>
	 *   <li>ポイと魚が衝突した場合の処理</li>
	 * </ol>
	 */
	void OnTriggerEnter2D(Collider2D other){
		if (senstickStatus) {
			//魚デストロォイ！！

		}
	}
		


	/**
	 * <ol>
	 *   <li>カメラ情報からポイの位置を取得する</li>
	 * </ol>
	 */
	void getPosition(){
		//SetPosition (float x, float y)にて座標を操作

		//return GetWorldMin();	//便宜上の適当な返り値です。気にせずに
	}
}

