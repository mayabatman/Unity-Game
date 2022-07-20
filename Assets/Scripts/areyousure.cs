using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//скрипт закреплен за объектом в сцене areyousure

public class areyousure : MonoBehaviour
{
    public void yes()
    {
        Application.Quit(); //выход из игры
    }

    public void no()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single); //выход в главное меню
    }
}
