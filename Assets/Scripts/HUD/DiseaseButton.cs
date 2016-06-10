using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DiseaseButton : MonoBehaviour
{
    static public List<DiseaseButton> buttons = new List<DiseaseButton>();
    static private int count;

    [Header("Components")]
    [SerializeField]
    private Text counter;
    [SerializeField]
    private GameObject counterObject;

    [Header("Disease")]
    [SerializeField]
    private Sprite icon;
    [SerializeField]
	private Infector infector;

    private int units;
    private Button button;

    void Awake()
    {
        buttons.Add(this);
        button = GetComponent<Button>();
        //Apenas para teste
        AddUnit(1);
    }

	public int GetUnits() {
		return units;		
	}

    public void AddUnit(int mod)
    {
        units += mod;

        try
        {

            if (units <= 0)
            {
                counterObject.gameObject.SetActive(false);
                button.interactable = false;
            }
            else
            {
                counterObject.gameObject.SetActive(true);
                button.interactable = true;
                counter.text = units.ToString();
            }
        }
        catch
        {
            Debug.LogError("Objeto do botao foi destruido (wtf)");
        }
    }

    public void MouseEnter()
    {
		InfoPanel.Show(infector.disease.name, infector.disease.description);
    }

    public void MouseExit()
    {
        InfoPanel.Hide();
    }

    public void MouseUp()
    {
        if (units > 0)
            DragContainer.Enable(icon, InstantiateDisease);
    }

    public bool InstantiateDisease(Vector2 position)
    {
        if (units <= 0)
            return false;

        AddUnit(-1);
		if (infector.InstantiateInfector ()) {
			Instantiate (infector, (new Vector3 (position.x, position.y, 0)), Quaternion.identity);	
		} else {
			Instantiate (infector.disease, (new Vector3 (position.x, position.y, 0)), Quaternion.identity);
		}


        return true;
    }

    static public void AddDisease()
    {
        count++;

        if (count >= 15)
        {
            buttons[Random.Range(0, buttons.Count-1)].AddUnit(1);
            count = 0;
        }
    }
}