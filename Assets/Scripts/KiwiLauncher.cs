using UnityEngine;
using System.Collections;

public class KiwiLauncher : MonoBehaviour {
	public GameObject Kiwi;
	
	public AudioClip aimSound;
	public AudioClip releaseSound;
		
	public float handicap = 50;
	
	// Use this for initialization
	void Start () {
		
	}
	
	private float power;
	private bool down = false;
	
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
		
		clone = Instantiate(Kiwi,transform.position,transform.rotation) as GameObject; 
		clone.rigidbody.useGravity = true;
		clone.rigidbody.AddForce(ray.direction*(magnitude*handicap), ForceMode.Impulse);
	}
	
	// Update is called once per frame
	void Update () {
		int i = 0;
        while (i < Input.touchCount) {
			if (Input.GetTouch(i).phase == TouchPhase.Began) {
				power = Time.time;
				
				AudioSource.PlayClipAtPoint(aimSound, transform.position);
			}
            if (Input.GetTouch(i).phase == TouchPhase.Ended) {
				power = Time.time - power;
				launchClone(Input.GetTouch(i).position.x,Input.GetTouch(i).position.y,power);
				
				AudioSource.PlayClipAtPoint(releaseSound, transform.position);
			}
            ++i;
        }
	}
}
