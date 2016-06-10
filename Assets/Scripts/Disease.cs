using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Disease : MonoBehaviour {

	static public int diseases = 0;

    [Header("Disease")]
    [SerializeField]
    public Color color;
    [SerializeField]
	public string description;
    [SerializeField]
    public LayerMask mask;
    [SerializeField]
    public int damage;
    [SerializeField]
    public int count;
    [SerializeField]
    public GameObject diseaseIconPrefab;

    internal GameObject icon;
    internal Cell host;

    internal virtual void Start() {
		SetHost(GetComponent<Cell>());
		Disease.diseases++;
        AddIcon();
    }

	public void SetHost (Cell h) {
		host = h;
	}

	internal virtual void ApplyEffect()
    {
        if (count <= 0)
        {
            EndEffect();
            return;
        }
        host.Damage(-damage);
        count--;
    }

	public virtual void EndEffect() {
		if (host != null) {
	        host.RemoveDisease(this);
		}
		Destroy(icon);
		Destroy(this);
		diseases--;
		Util.CheckForGameOver ();
    }

    internal void AddIcon()
    {
		if (host == null) {
			return;
		}

        icon = Instantiate<GameObject>(diseaseIconPrefab);
        icon.transform.SetParent(host.diseasePanel.transform);
        icon.name = name;
        icon.GetComponent<Image>().color = color;
        icon.transform.localScale = new Vector2(1, 1);
    }

    public IEnumerator Updater()
    {
        while (true)
        {
            ApplyEffect();
            yield return new WaitForSeconds(2f);
        }
    }
}
