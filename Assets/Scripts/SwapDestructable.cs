using UnityEngine;
using System.Collections;

public class SwapDestructable : MonoBehaviour {
	public float hp = 100;
	public GameObject fracturedObject;
	
	// Use this for initialization
	void Start () {

	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.collider.rigidbody) {
	        float damage = collision.collider.rigidbody.mass * collision.relativeVelocity.magnitude;
			hp = hp - damage;
			
			Debug.Log("HP:" + hp);
			
			if (hp <= 0) {
				GameObject swappedPillar;

		        swappedPillar = Instantiate(fracturedObject, gameObject.transform.position, fracturedObject.transform.rotation) as GameObject;
				
		        Destroy(gameObject);
				Destroy(swappedPillar, 5);
			}
		}
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
