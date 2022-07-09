using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script3 : MonoBehaviour
{
    public GameObject[] objects;
    private GameObject inst_obj;
    
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range (0, objects.Length -1); 
        inst_obj = Instantiate(objects[rand], objects[rand].transform.position, objects[rand].transform.rotation) as GameObject;
        inst_obj.transform.Translate(Vector2.up*25f);
    }
}
