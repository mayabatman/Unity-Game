using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public bool HasHeighbour;
    public bool HasEnemy;

    // Update is called once per frame
    void Update()
    {
        if(!HasEnemy){ //если в комнате враг, дверь в любом случае должна быть закрыта
        if (HasHeighbour)
        {
            gameObject.SetActive (false); //открыть
        }
        else gameObject.SetActive (true); //закрыть
        }
    }
}
