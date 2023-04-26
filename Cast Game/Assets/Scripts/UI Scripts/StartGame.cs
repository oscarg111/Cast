using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartLevel() {
        Debug.Log("Clicked");
        SceneManager.LoadScene("Character Select");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void goToControls()
    {
        SceneManager.LoadScene("Controls");
    }
    public void goToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
