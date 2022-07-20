using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
//вывод данных о рекордах
public class Records : MonoBehaviour
{
    public TMP_Text[] rec;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            rec[i].text = (i+1).ToString()+".    " + PlayerPrefs.GetInt((i+1).ToString()).ToString();
        }
    }

    public void Back() //функция для кнопочки назад
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
    }


}
