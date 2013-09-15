using UnityEngine;
using System.Collections;
using System;

public class HudServer : MonoBehaviour {
	public GameObject Kiwi;
	public AudioClip aimSound;
	public AudioClip releaseSound;
		
	public float handicap = 50;
	public int port = 2500;
	
	public GUITexture reticle;
	public Camera camera;
	
	private float power;
	private bool down = false;
	private bool launchKiwi = false;
	
	enum sensors { bottomLeft , topLeft, bottomRight , topRight};
	
	void OnGUI() {
        Event e = Event.current;
		
		if (e.type == EventType.KeyDown && down == false) {
			if (e.isKey) {
				if (e.character == 'x') {
					//Debug.Log("Key Down" + Time.time);
					power = Time.time;
					down = true;
					
					AudioSource.PlayClipAtPoint(aimSound, transform.position);
				}
			}
		}
		else if (e.type == EventType.KeyUp && down == true) {
        	if (e.isKey) {
            	//Debug.Log("Detected character: " + e.character);
				//Debug.Log("Key Up" + Time.time);
				power = Time.time - power;
				//Debug.Log("Power" + power);
				launchClone(e.mousePosition.x, camera.pixelHeight - e.mousePosition.y, power);
				down = false;
				
				AudioSource.PlayClipAtPoint(releaseSound, transform.position);
			}
		}
    }
	
	void launchClone(float positionX,float positionY,float magnitude) {
		GameObject clone;
		
		Ray ray = camera.ScreenPointToRay(new Vector3(positionX, positionY, 0));
		
		clone = Instantiate(Kiwi,camera.transform.position,camera.transform.rotation) as GameObject; 
		clone.rigidbody.useGravity = true;
		clone.rigidbody.AddForce(ray.direction*(magnitude*handicap), ForceMode.Impulse);
		
	}
	
	int[] parseRawData (string data) {
		int[] intValues = new int[4];
		
		char[] delimiterChars = { ','};
		string[] stringValues = data.Split(delimiterChars);
		
		if (stringValues.Length == 5) {
			launchKiwi = true;

			for (int i = 1; i < 5; i++) {
				intValues[i-1] = Int32.Parse(stringValues[i]);
			}
			
			return intValues;
		}

		for (int i = 0; i < 4; i++) {
			intValues[i] = Int32.Parse(stringValues[i]);
		}
		
		return intValues;
	}
	
	float xPosition (float[] values) {
		return ((values[(int)sensors.topLeft]*-.5f + 
			values[(int)sensors.bottomLeft]*-.5f + 
			values[(int)sensors.topRight]*.5f + 
			values[(int)sensors.bottomRight]*.5f)/2.0f)+0.5f;
	}
	
	float yPosition (float[] values) {
		return ((values[(int)sensors.bottomRight]*-.5f + 
			values[(int)sensors.bottomLeft]*-.5f + 
			values[(int)sensors.topRight]*.5f + 
			values[(int)sensors.topLeft]*.5f)/2.0f)+0.5f;
	}
	
	// Use this for initialization
	void Start () {
		if( !guiText ) {
	        Debug.Log("UtilityHudServer needs a GUIText component!");
	        enabled = false;
	        return;
    	}
		else {
			guiText.material.color = Color.blue;
			
			UdpServer.init(port);
			if (UdpServer.startStop() == 1) {
				Debug.Log("Server Started");	
			} 
			else {
				Debug.Log(UdpServer.getMsg());
			}
		}
	}
	
	// Update is called every ten frames
	void Update () {
		if (Application.platform != RuntimePlatform.OSXEditor) {
			//if (Time.frameCount % 10 == 0) {
			//Debug.Log("iPad");
			string msg = UdpServer.getMsg();
			if (!msg.Equals(""))
			{
				guiText.text = msg; 
				
				float min = 1024;
				float max = 0;
				
				int[] stretchSensors = parseRawData(guiText.text);
				foreach (int sensor in stretchSensors) {
					if (sensor < min)
						min = sensor;
					if (sensor > max)
						max = sensor;
				}
				
				Debug.Log("Sensors: "+stretchSensors[0]+","+stretchSensors[1]+","+stretchSensors[2]+","+stretchSensors[3]);
				
				float[] normalizedSensors = new float[]{stretchSensors[0]/(450-min),stretchSensors[1]/(450-min),stretchSensors[2]/(450-min),stretchSensors[3]/(450-min)};
				float normalizedPower = max/(1024);
				
				Vector2 screenPositionReticle = new Vector2(xPosition(normalizedSensors)*Screen.width-Screen.width/2,yPosition(normalizedSensors)*Screen.height-Screen.height/2);
				Rect center = new Rect(0,0,64,64);
				center.center = screenPositionReticle;
				reticle.pixelInset = center;
				
				if (launchKiwi) {
					Vector2 screenPosition = new Vector2(xPosition(normalizedSensors)*Screen.width,yPosition(normalizedSensors)*Screen.height);
					launchClone(screenPosition.x, screenPosition.y, normalizedPower*2);
					launchKiwi = false;
				}
			}
		}
		else {
			//Debug.Log("mac");
			float min = 1024;
			float max = 0;
			
			int[] stretchSensors = new int[]{376,343,422,384};
			foreach (int sensor in stretchSensors) {
				if (sensor < min)
					min = sensor;
				if (sensor > max)
					max = sensor;
			}
			
			float[] normalizedSensors = new float[]{stretchSensors[0]/(max-min),stretchSensors[1]/(max-min),stretchSensors[2]/(max-min),stretchSensors[3]/(max-min)};			
			float normalizedPower = 350/(1024);
			
			Vector2 screenPosition = new Vector2(xPosition(normalizedSensors)*Screen.width-Screen.width/2,yPosition(normalizedSensors)*Screen.height-Screen.height/2);
			Rect center = new Rect(0,0,64,64);
			center.center = screenPosition;
			reticle.pixelInset = center;
			
			if (launchKiwi) {
					launchClone(screenPosition.x, screenPosition.y, normalizedPower*5);
					launchKiwi = false;
			}
		}
	}
}
