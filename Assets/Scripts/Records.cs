using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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

    public void Back()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
