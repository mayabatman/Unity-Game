using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//функция для кнопки в меню "ура ты победил"
public class NextLeve : MonoBehaviour
{
    public void ToTheMainMenu()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
    }
}