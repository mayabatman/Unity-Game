using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemy_1;
        enemy_1 = GameObject.FindGameObjectsWithTag("Enemy_1");
        GameObject[] enemy_2;
        enemy_2 = GameObject.FindGameObjectsWithTag("Enemy_2");

        if (enemy_1.Length == 0 && enemy_2.Length == 0)
        {
            gameObject.SetActive (false);
        }
        else
        {
            gameObject.SetActive (true);
        }

    }
}
