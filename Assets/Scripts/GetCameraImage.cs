using UnityEngine;
using System.Collections;

public class GetCameraImage : MonoBehaviour {
	private int height = 1024;
	private int width = 768;
	
	private Vector2 flippedScale = new Vector2(-1f,1f);
	
	public static WebCamTexture webCamTexture;

	// Use this for initialization
	void Start () {
		if (!webCamTexture) {	
			var deviceName = WebCamTexture.devices[0].name;
			
			webCamTexture = new WebCamTexture(deviceName, height, width, 30);
			webCamTexture.Play();
		}
		renderer.material.SetTexture("_Detail",webCamTexture);
		renderer.material.SetTextureScale("_Detail",flippedScale);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
