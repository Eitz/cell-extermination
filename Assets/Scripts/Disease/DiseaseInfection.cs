using UnityEngine;
using System.Collections;

public class DiseaseInfection : Disease {

	[SerializeField]
	public InfectorInfeccao infector;

	internal override void Start () {
		SetHost(GetComponent<Cell>());
		StartCoroutine(Updater());
		base.Start ();
	}

	public override void EndEffect () {
		if (infector != null) {
			if (20 > Random.Range (0, 100)) {
				Instantiate (infector, host.transform.position, Quaternion.identity);
			}

		}
		base.EndEffect ();
	}	
}
