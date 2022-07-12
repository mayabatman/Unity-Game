using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    public bool HasHeighbour;
    public bool HasEnemy;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       // print("статус дверь обновляется N "+HasHeighbour+" E "+HasEnemy);
        
        if (HasHeighbour)
        {
            gameObject.SetActive (false);
        }
        else gameObject.SetActive (true);
    }
}
