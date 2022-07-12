using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class script2 : MonoBehaviour
{
    public GameObject obj;
    public float range = 5f, moveSpeed = 5f;



    void Update() {/*
        float h = Input.GetAxis ("Horizontal");
        float xPos = h*range;

        obj.transform.position = new Vector2 (xPos, 0);*/

       // gameObject.GetComponent<script>().Func();


        if(Input.GetKey(KeyCode.W))
            obj.transform.Translate(Vector2.up*moveSpeed*Time.deltaTime);
        if(Input.GetKey(KeyCode.S))
            obj.transform.Translate(-Vector2.up*moveSpeed*Time.deltaTime);
        if(Input.GetKey(KeyCode.A))
            obj.transform.Translate(Vector2.left*moveSpeed*Time.deltaTime);
        if(Input.GetKey(KeyCode.D))
            obj.transform.Translate(-Vector2.left*moveSpeed*Time.deltaTime);

        if(Input.GetKey(KeyCode.Space))
            transform.localScale = new Vector2 (transform.localScale.x*2f, transform.localScale.y*2f);
    }
}