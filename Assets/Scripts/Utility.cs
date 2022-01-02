using UnityEngine;
using UnityEngine.SceneManagement;

public class Utility : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit() => Application.Quit();
    
}
