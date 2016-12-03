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
	private int userPoint = 0;

	/**
	 * senstickの振られたかどうか(振られたらtrue)
	 */
	private bool senstickStatus = false;

	/**
	 * ポイの名前
	 */
	public string myName;

	/*
	 * ポイの座標
	 */
	private List<float> poiCoordinate  = new List<float> (){0.0f, 0.0f};

	/*
	 * ポイ座標のノイズ除去変数
	 */
	private float noiseFiltering = 0.05;


	//test field
	private float x;
	private float y;



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
		x = -2f;
		y = 2f;
		poiCoordinate = new List<float> (){0.0f, 0.0f};

	}


	
	/**
	 * <ol>
	 *   <li>1フレームごとの処理</li>
	 * </ol>
	 */
	void Update () {
		//var obj = GetComponent<WS>.obj;

		/*
		if (x > 2f) {
			x = -2f;
			y = y - 1f;
		}
		if (y < -2f) {
			y = 2f;
		}
		setPoiPosition (x, y);

		//Debug.Log ("x:" + x + "\ty:" + y);

		x = x + 0.1f;
		*/

		Debug.Log ("status:"+senstickStatus);
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
			//Debug.Log("Name:" + item.id + ", state:" + item.state + ", myName:" + myName);
			//if(myName == item.id && item.state == "1"){
				//Debug.Log("同じ名前と結果やんけ！"+ myName);
			if(item.state == "1"){
				senstickStatus = true;
				Debug.Log("Name:" + item.id + ", state:" + item.state + ", myName:" + myName);
			}
			else if(myName == item.id && item.state=="0"){
				//Debug.Log("違うのかよ!");
				senstickStatus = false;
			}
		}

	}



	/**
	 * <ol>
	 *   <li>ユーザのポイントを加点するかどうかの判定</li>
	 * </ol>
	 */
	public void getpoint(){
		//加点するポイントが5点固定だとした場合
		userPoint += 5;
	}

	/**
	 * <ol>
	 *   <li>ポイと魚が衝突した場合の処理</li>
	 * </ol>
	 */
	private void OnTriggerStay2D(Collider2D other){
		Debug.Log ("kitayo:"+senstickStatus);
		if (senstickStatus) {

			Debug.Log ("tag:"+other.tag+"\tsenstick:"+senstickStatus);
			//魚デストロォイ！！
			GameObject.Destroy(other.gameObject);
		}
	}
		


	/**
	 * <ol>
	 *   <li>カメラ情報からポイの位置を取得する</li>
	 * </ol>
	 */
	public void setPoiPosition(float[] ArrayPosition){
		//SetPosition (float x, float y)にて座標を操作




		Debug.Log ("test");


		if (myName == "number0") {
			if ((ArrayPosition [0] != 10000) && (ArrayPosition [1] != 10000)) {
				noiseFilter (ArrayPosition [0], ArrayPosition [1]);
				SetPosition (poiCoordinate [0], poiCoordinate [1]);
			}
		}
		else if (myName == "number1") {
			if ((ArrayPosition [2] != 10000) && (ArrayPosition [3] != 10000)) {
				noiseFilter (ArrayPosition [0], ArrayPosition [1]);
				SetPosition (poiCoordinate [2], poiCoordinate [3]);
			}
		}
		else if(myName == "number2"){
			if ((ArrayPosition [4] != 10000) && (ArrayPosition [5] != 10000)) {
				noiseFilter (ArrayPosition [0], ArrayPosition [1]);
				SetPosition (poiCoordinate [4], poiCoordinate [5]);
			}
		}
	}
		
	/**
		 * <ol>
		 * 	<li>ポイ座標の急激な変化をなめらかに描画する
		 * <ol>
		 */
	public void noiseFilter(float poiCoordinateX, float poiCoordinateY){

		poiCoordinate [0] += (poiCoordinateX - poiCoordinate [0]) * noiseFiltering;
		poiCoordinate [1] += (poiCoordinateY - poiCoordinate [1]) * noiseFiltering;
	}

}

