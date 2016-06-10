using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(SphereCollider))]
public class Explosion : MonoBehaviour {

	private SphereCollider myCollider;
	public Transform particles;
	private float time;
	// private List<GameObject> generatedParticles = new List<GameObject>();

	void Start () {
		myCollider = transform.GetComponent<SphereCollider>();
		time = Time.time + 0.01f;
	}
	void Update ()	{
	
		if (Time.time >= time) {
			time = Time.time + 0.01f;
			if (myCollider.radius < 10) {
				Instantiate(particles, transform.position, transform.rotation);
				// b.transform.parent = this.transform;
				//generatedParticles.Add(b);
				myCollider.radius += 1f;
			}
			else {
				myCollider.enabled = false;
				/*
				foreach (GameObject g in generatedParticles) {
					
					Destroy (g);
				}
				*/
				Destroy (this);
			}

		}
	}
}