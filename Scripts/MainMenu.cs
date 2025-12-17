using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsWindow;

    public void Play()
    {
        SceneManager.LoadScene("LogMenu"); 
    }

    public void Settings()
    {
        settingsWindow.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsWindow.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
