using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Cell : MonoBehaviour {

    static private List<Cell> cells = new List<Cell>();

    [SerializeField]
    private float velocity;
    [SerializeField]
    public Transform rotateAxys;
    [SerializeField]
    private SpriteRenderer myRenderer;
    [SerializeField]
    public GameObject diseasePanel;

    public Color defaultColor = new Color (1, 1, 1);
    [SerializeField]
    private int health;
	private List<Disease> diseases = new List<Disease> ();

    [SerializeField]
	private Transform ground;
	Vector3 moveTarget;

	void Start () {
		myRenderer.color = defaultColor;
		health = Random.Range(10,40);
        CalculateScale();
        //ground = GameObject.FindGameObjectWithTag ("Ground").GetComponent<Transform> ();
		moveTarget = SetNewTarget ();
        cells.Add(this);
		// AdjustSize ();
	}
    
	void FixedUpdate () {
		if (ArrivedAtTarget()) {
			moveTarget = SetNewTarget();
		}
		MoveToTarget();
	}

	public void AddDisease (Disease d) {
        if (d == null)
        {
            Debug.LogError("Disease veio null");
            return;
        }

        Disease myDisease = gameObject.GetComponent(d.GetType()) as Disease;

        if (myDisease != null) {
            return;
        }

        Disease addedDisease = CopyComponent<Disease>(d);
        diseases.Add(addedDisease);
        myRenderer.color = CalculateColor();
    }

    T CopyComponent<T>(T original) where T : Component
    {
        System.Type type = original.GetType();
        Component copy = gameObject.AddComponent(type);
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }

        return copy as T;
    }

	public void RemoveDisease (Disease d) {

        diseases.RemoveAt(diseases.IndexOf(d));
        myRenderer.color = CalculateColor();
	}

	private bool ArrivedAtTarget() {
		Vector3 currentPosition = new Vector3((int)(transform.position.x), (int)(transform.position.y), 0);
		return (Vector3.Distance (currentPosition, moveTarget) < 5);
	}

	public void Split () {
		// TODO
	}

	public Vector3 SetNewTarget() {
		float sizeX = myRenderer.bounds.size.x / 2;
		float sizeY = myRenderer.bounds.size.x / 2;
		int x = Random.Range (-(int)(ground.localScale.x/2 - sizeX), (int)(ground.localScale.x/2 - sizeX));
		int y = Random.Range (-(int)(ground.localScale.y/2 - sizeY), (int)(ground.localScale.y/2 - sizeY));
		Vector3 target = new Vector3 (x, y, 0);
		Vector3 diff = target - transform.position;
		diff.Normalize();
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		rotateAxys.rotation = Quaternion.Euler(0f, 0f, rot_z-90);
		return target;
	}

	public void MoveToTarget() {
		float newVelocity = velocity - (rotateAxys.localScale.x * 0.3f);
		transform.position += (rotateAxys.up * newVelocity) * Time.deltaTime;
	}

	private Color CalculateColor() {
		List<Color> colors = new List<Color>();

        if (diseases.Count == 0)
        {
            return defaultColor;
        }
        else
        {
            return diseases[diseases.Count-1].color;
        }
        /*foreach (Disease d in diseases) {
			if (!colors.Contains (d.color)) {
				colors.Add (d.color);
			}
		}
		Color result = new Color(0,0,0,0);
		foreach(Color c in colors)
		{
			result += c;
		}
		result /= colors.Count;
        //Garantir que nao venha opaco
        result.a = 255;*/
	}

    private void CalculateScale()
    {
        float scale;
        if (health < 20)
            scale = (float)20 / (float)40;
        else
            scale = (float)health / (float)40;
        
        transform.localScale = new Vector3(scale, scale, scale);
    }

    public void Damage(int damage)
    {
        health += damage;
		// AdjustSize ();

        if (health <= 1) {
            if (cells.Contains(this))
                cells.RemoveAt(cells.IndexOf(this));
			for (int i=0; i < diseases.Count; i++) {
				Disease.diseases--;
			}
			if (cells.Count < 1) {
                WinPanel.Win();
			}
			print ("Cell death");
			Util.CheckForGameOver ();
            Destroy(gameObject);
            DiseaseButton.AddDisease();
            return;
        }

        CalculateScale();
    }
}