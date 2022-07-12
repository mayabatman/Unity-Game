using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class script : MonoBehaviour
{
    void Start () {

    }

    void Update() {

        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene("menu", LoadSceneMode.Additive);
        }
    }

    public void Func()
    {
        //if (Input.GetKeyUp (KeyCode.Space))
        
    }
}

