using UnityEngine;
using System.Collections;

public class LlamaDeath : MonoBehaviour {
	public GameObject LlamaDeathParticleSystem;
	public AudioClip LlamaDeathSound;
	
	public float hp = 100;
	
	private ParticleSystem deathParticles;
	private bool colorChanged = false;
	
	private ParticleSystem.Particle[] particles;
	
	// Use this for initialization
	void Start () {
		particles = new ParticleSystem.Particle[1000];
	}
	
	IEnumerator death() {	
		yield return new WaitForSeconds(.4f);
		
		deathParticles = Instantiate(LlamaDeathParticleSystem.GetComponent<ParticleSystem>(),transform.position,LlamaDeathParticleSystem.transform.rotation) as ParticleSystem;
		Destroy(deathParticles, 2.5F);
		
		AudioSource.PlayClipAtPoint(LlamaDeathSound, transform.position);
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.collider.rigidbody) {
	        float damage = collision.collider.rigidbody.mass * collision.relativeVelocity.magnitude;
			hp = hp - damage;
			
			//Debug.Log("Llama HP:" + hp);
			
			if (hp <= 0) {
				Destroy(gameObject, 0.5F);
				StartCoroutine(death());
			}
		}
    }
	
	// Update is called once per frame
	void Update () {
		
		if (deathParticles) {
			int size = deathParticles.GetParticles(particles);
			
			if (size > 0 && !colorChanged) {			
				int alive = 0;
		    	
				while (alive < size) {
					Color randomColor = new Color(Random.Range(0.0F,1.0F),Random.Range(0.0F,1.0F),Random.Range(0.0F,1.0F));
		        	
					particles[alive].color = randomColor;
		        	particles[alive].size = Random.Range(0.2F,2.0F);
		        	alive++;
		    	}
			
				deathParticles.SetParticles(particles,size);
				colorChanged = true;
			}
		}
	}
}
