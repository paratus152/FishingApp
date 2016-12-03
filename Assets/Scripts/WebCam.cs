using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.IO;

public class WebCam : MonoBehaviour {

	public int Width = 1920;
	public int Height = 1080;
	//public int Width = 1280;
	//public int Height = 800;
	public int FPS = 30;
	public Color32[] data;
	public WebCamTexture webcamTexture;


	//Poi
	private GameObject poiObject;
	private Poi GetPoi1;
	private Poi GetPoi2;
	private Poi GetPoi3;

	private int count;

	public void textSave(string txt){
		StreamWriter sw = new StreamWriter ("/../LogData.txt", true);
		sw.WriteLine (txt);
		sw.Flush();
		sw.Close();
	}


	// Use this for initialization
	void Start () {
		WebCamDevice[] devices = WebCamTexture.devices;
		for (var i = 0; i < devices.Length; i++) {
			Debug.Log ("i:"+i+"\t"+devices [i].name);
		}

		webcamTexture = new WebCamTexture (devices[1].name, Width, Height, FPS);
		GetComponent<Renderer> ().material.mainTexture = webcamTexture;
		webcamTexture.Play ();
		Debug.Log (webcamTexture.width);
		Debug.Log (webcamTexture.height);
		data = new Color32[webcamTexture.width * webcamTexture.height];

		//initialization of Poi
		poiObject = GameObject.Find ("Poi1");
		GetPoi1 = poiObject.GetComponent<Poi>();
		poiObject = GameObject.Find ("Poi2");
		GetPoi2 = poiObject.GetComponent<Poi>();
		poiObject = GameObject.Find ("Poi3");
		GetPoi3 = poiObject.GetComponent<Poi>();
	}

	void Update(){
		data = webcamTexture.GetPixels32 ();


		//Debug.Log ("i:"+count+"\tlength:"+data.Length);

		count++;

		List<float> v = new List<float>(){10000, 10000, 10000, 10000, 10000, 10000}; 

		for (int i = 0; i < 800; i++){
			for (int j = 0; j < 1280; j++) {

				if (data [i * 1280 + j].r > 50 && data [i * 1280 + j].g > 50 && data [i * 1280 + j].g > 50) {
					data [i * 1280 + j].r = 255;
					data [i * 1280 + j].g = 0;
					data [i * 1280 + j].b = 0;
					Debug.Log ("x;"+j+" y;"+i);

					if (0 < j && j < 640 && 0 < i && i < 400) {
						v[2] = ((i - 400)/800.0f) * 1900;
						v[3] = ((j - 640)/1280.0f) * 4000;
					}
					else if (640 < j && j <1280 && 0 <= i && i < 400) {
						v[4] = ((i - 400)/800.0f) * 1900;
						v[5] = ((j - 640)/1280.0f) * 4000;
					}
					else if (0 <= j && j < 640 && 400 <= i && i < 800) {
						v[0] = ((i - 400)/800.0f) * 1900;
						v[1] = ((j - 640)/1280.0f) * 4000;
					}
				}
			}
		}

		float[] array;
		array = v.ToArray();

		if ( (array [0] != 10000) && (array [1] != 10000) ) {
			Debug.Log ("Poi1\tx;"+array[0]+" y;"+array[1]);
			GetPoi1.setPoiPosition (array);
		}
		if ((array [2] != 10000) && (array [3] != 10000)) {
			Debug.Log ("Poi2\tx;"+array[2]+" y;"+array[3]);
			GetPoi2.setPoiPosition (array);
		}
		if ((array [4] != 10000) && (array [5] != 10000)) {
			Debug.Log ("Poi3\tx;"+array[4]+" y;"+array[5]);
			GetPoi3.setPoiPosition (array);
		}



		//Debug.Log (array[0]+","+array[1]+","+array[2]+","+array[3]+","+array[4]+","+array[5]);

		//Debug.Log (v[0]+","+v[1]+","+v[2]+","+v[3]+","+v[4]+","+v[5]);

	}
}
