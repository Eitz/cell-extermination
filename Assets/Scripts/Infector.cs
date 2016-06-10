using UnityEngine;
using System.Collections;

public class Infector : MonoBehaviour {

	[SerializeField]
	public Disease disease;

	internal virtual void Start () {
		Disease.diseases++;
	}

	public virtual bool InstantiateInfector () {
		return false;
	}
}
