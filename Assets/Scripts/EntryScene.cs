using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryScene : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }
}
