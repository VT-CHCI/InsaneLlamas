using UnityEngine;
using System.Collections;

public class KiwiDeath : MonoBehaviour {
	public GameObject KiwiDeathParticleSystem;
	public GameObject KiwiCollisionParticleSystem;
	private bool firstCollision = true;
	
	// Use this for initialization
	void Start () {
	
	}
	
	IEnumerator initDeath() {
		yield return new WaitForSeconds(4.9f);
		
		GameObject deathParticles;
			
		deathParticles = Instantiate(KiwiDeathParticleSystem,transform.position,KiwiDeathParticleSystem.transform.rotation) as GameObject;
		Destroy(deathParticles, 5);
	}
	
	void OnCollisionEnter(Collision collision) {
		/*
        foreach (ContactPoint contact in collision.contacts) {
			GameObject collisionParticles;
			
			collisionParticles = Instantiate(KiwiCollisionParticleSystem,transform.position,transform.rotation) as GameObject;
			Destroy(collisionParticles, 1);
        }
        */
		
		if (firstCollision) {
			Destroy(gameObject, 5);	
			StartCoroutine(initDeath());
			firstCollision = false;
		}
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
