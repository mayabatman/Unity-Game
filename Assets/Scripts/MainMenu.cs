using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// главное меню
//функции для кнопок в главном меню

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
        SceneManager.LoadScene("areyousure", LoadSceneMode.Single);
    }
}
