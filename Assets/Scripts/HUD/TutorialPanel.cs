using UnityEngine;
using System.Collections;

public class TutorialPanel : MonoBehaviour
{
    void Awake()
    {
        Time.timeScale = 0f;
    }

    public void Play()
    {
        Time.timeScale = 1f;
    }
}
