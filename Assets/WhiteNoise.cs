using UnityEngine;
using System.Collections;

public class WhiteNoise : MonoBehaviour {
	private static Texture2D mTexture = null;
	private const int w = 256;
	private const int h = 192;
	
	public GUITexture texture;
	
	// Use this for initialization
	void Start () {
		mTexture = new Texture2D(w, h, TextureFormat.RGB24, false);
        mTexture.filterMode = FilterMode.Bilinear;
        mTexture.wrapMode = TextureWrapMode.Clamp;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("Tracked objects: "+DefaultTrackableEventHandler.numTrackers);
		if (DefaultTrackableEventHandler.numTrackers < -27) {
			texture.enabled = true;
			
			for (int i = 0; i < w; i++) {
				for (int j = 0; j < h; j++) {
					int randomNumber = Random.Range(0,5);
					float random = ((float)randomNumber)*0.25f;
					mTexture.SetPixel(i,j,new Color(random,random,random));
				}
			}
			mTexture.Apply();
			
			texture.texture = mTexture;
		}
		else texture.enabled = false;
	}
}
