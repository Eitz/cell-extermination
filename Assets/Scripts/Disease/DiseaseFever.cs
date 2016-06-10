using UnityEngine;
using System.Collections;

public class DiseaseFever : Disease {
	internal override void Start () {
		SetHost(GetComponent<Cell>());
		StartCoroutine(Updater());
		base.Start ();
	}
}
