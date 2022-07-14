using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("base", LoadSceneMode.Single);
    }

    public void Records()
    {
        SceneManager.LoadScene("records", LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Debug.Log("ВЫХОД");
        Application.Quit();
    }
}
