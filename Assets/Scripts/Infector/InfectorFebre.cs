using UnityEngine;
using System.Collections;

public class InfectorFebre : Infector {

	[SerializeField]
	private float velocity;
	[SerializeField]
	private Transform rotateAxis;
	[SerializeField]
	private SpriteRenderer myRenderer;
	[SerializeField]
	private int targetCountBase = 5;
	private int targetCount;

	private Transform ground;
	Vector3 moveTarget;

	public override bool InstantiateInfector () {
		return true;
	}

	internal override void Start () {		
		targetCount = targetCountBase;
		ground = GameObject.FindGameObjectWithTag ("Ground").GetComponent<Transform> ();
		if (ground == null) {
			Debug.LogError("Ground nao encontrado");
			return;
		}
		moveTarget = SetNewTarget ();
		base.Start();
	}

	void FixedUpdate () {
		if (ArrivedAtTarget()) {
			moveTarget = SetNewTarget();
		}
		MoveToTarget();		
	}

	void OnTriggerEnter2D(Collider2D other) {
		Cell cell = other.gameObject.GetComponent<Cell>();
		if (cell != null) {
			cell.AddDisease (disease);
			moveTarget = SetNewTarget();
			targetCount = targetCountBase;
		}
	}

	private bool ArrivedAtTarget() {
		Vector3 currentPosition = new Vector3((int)(transform.position.x), (int)(transform.position.y), 0);
		return (Vector3.Distance (currentPosition, moveTarget) < 5);
	}

	public Vector3 SetNewTarget() {
		targetCount--;
		if (targetCount <= 0) {
			GameObject.Destroy (this.gameObject);
			Disease.diseases--;
			Util.CheckForGameOver ();
		}
		float sizeX = myRenderer.bounds.size.x / 2;
		float sizeY = myRenderer.bounds.size.x / 2;
		int x = Random.Range (-(int)(ground.localScale.x/2 - sizeX), (int)(ground.localScale.x/2 - sizeX));
		int y = Random.Range (-(int)(ground.localScale.y/2 - sizeY), (int)(ground.localScale.y/2 - sizeY));
		Vector3 target = new Vector3 (x, y, 0);
		Vector3 diff = target - transform.position;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		rotateAxis.rotation = Quaternion.Euler(0f, 0f, rot_z-90);
		return target;
	}

	public void MoveToTarget() {
		float newVelocity = velocity - (transform.localScale.x * 0.1f);
		transform.position += (rotateAxis.up * newVelocity) * Time.deltaTime;
	}
}
