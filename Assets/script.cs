using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script : MonoBehaviour
{
    public GameObject obj;
    private Light myLight;

    void Start () {
        myLight = GetComponent <Light> ();
    }

    void Update() {
        /*if (Input.GetKeyUp (KeyCode.Space))
        {
            myLight.enabled = !myLight.enabled;
        }

        if (Input.GetKeyUp (KeyCode.A)) {
            obj.SetActive (false);
        }*/

        if(Input.GetKeyUp (KeyCode.R))
            obj.GetComponent <Renderer> ().material.color = Color.red;
        else if(Input.GetKeyUp (KeyCode.G))
            obj.GetComponent <Renderer> ().material.color = Color.green;
        else if(Input.GetKeyUp (KeyCode.B))
            obj.GetComponent <Renderer> ().material.color = Color.blue;
        
    }
}
