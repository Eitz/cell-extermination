using UnityEngine;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    static public WinPanel singleton;

    [Header("Components")]
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private Text title;
    [SerializeField]
    private Text description;
    [SerializeField]
    private Image icon;

    [Header("Mod")]
    [SerializeField]
    private Sprite loseIcon;
    [SerializeField]
    private Sprite winIcon;

    void Awake()
    {
        singleton = this;
    }

    static public void Win()
    {
        Time.timeScale = 0;
        singleton.panel.SetActive(true);
        singleton.icon.sprite = singleton.winIcon;
        singleton.title.text = "Você ganhou!";
        singleton.description.text = "Você conseguiu exterminar todas as células!";
    }

    static public void Lose()
    {
        Time.timeScale = 0;
        singleton.panel.SetActive(true);
        singleton.icon.sprite = singleton.loseIcon;
        singleton.title.text = "Você perdeu!";
        singleton.description.text = "Que pena, jogue novamente e tenta utilizar melhor a sequência de doenças.";
    }
}
