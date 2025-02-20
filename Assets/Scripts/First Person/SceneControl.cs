using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void restartLevel()
    {
        SceneManager.LoadScene("PrimeraPersona");
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
