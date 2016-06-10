using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    static private InfoPanel singleton;

    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text description;

    private void Awake()
    {
        singleton = this;
    }

    static public void Show(string title, string description)
    {
        singleton.title.text = title;
        singleton.description.text = description;

        singleton.panel.SetActive(true);
    }

    static public void Hide()
    {
        singleton.panel.SetActive(false);
    }
}