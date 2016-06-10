using UnityEngine;
using System.Collections;

[System.Serializable]
public class DiseaseInfluenza : Disease {

    [Header("Explosion")]
    [SerializeField]
	private float contaminationRadius = 5f;
    [SerializeField]
    public GameObject particle;

    private GameObject particleObject;

    override internal void Start () {
		SetHost(GetComponent<Cell>());
        if (host == null) {
			Disease.diseases++;
            Explode();
			Invoke ("EndEffect", 0.1f);
        }
        else
        {
            StartCoroutine(Updater());
            particleObject = Instantiate<GameObject>(particle);
            particleObject.transform.SetParent(host.transform);
            particleObject.transform.localPosition = Vector2.zero;
            particleObject.transform.localScale = new Vector3(1,1,1);
            particleObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
			base.Start();
        }
	}

 	internal override void ApplyEffect () {
        base.ApplyEffect();
    }

    internal void Explode()
    {
        Collider2D[] cells = Physics2D.OverlapCircleAll(transform.position, contaminationRadius, mask);
        foreach (Collider2D c in cells)
        {
            if (c.gameObject == gameObject)
                return;

            try
            {
                c.gameObject.GetComponent<Cell>().AddDisease(this);
            }
            catch
            {
                Debug.LogError("Doença foi destruida");
            }
        }
    }

	public override void EndEffect ()
    {	
		Debug.Log ("Chamei .1s");
		if (particleObject != null) {
			Destroy (particleObject);
		}
        base.EndEffect();
    }
}