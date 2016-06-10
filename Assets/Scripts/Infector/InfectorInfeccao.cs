using UnityEngine;
using System.Collections;

public class InfectorInfeccao : Infector {

	[SerializeField]
	private SpriteRenderer myRenderer;
	[SerializeField]
	private float lifeTime = 5f;

	public override bool InstantiateInfector () {
		return true;
	}

	internal override void Start () {
		StartCoroutine (KillMyself ());
		base.Start ();
	}

	IEnumerator KillMyself() {
		yield return new WaitForSeconds (lifeTime);
		Disease.diseases--;
		GameObject.Destroy (this.gameObject);
		Util.CheckForGameOver ();
	}



	void OnTriggerEnter2D(Collider2D other) {
		Cell cell = other.gameObject.GetComponent<Cell>();
		if (cell != null) {
			cell.AddDisease (disease);
		}
	}
}
