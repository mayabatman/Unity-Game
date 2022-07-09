using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float range = 5f, moveSpeed = 5f, health = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
            gameObject.transform.Translate(Vector2.up*moveSpeed*Time.deltaTime);
        if(Input.GetKey(KeyCode.S))
            gameObject.transform.Translate(-Vector2.up*moveSpeed*Time.deltaTime);
        if(Input.GetKey(KeyCode.A))
            gameObject.transform.Translate(Vector2.left*moveSpeed*Time.deltaTime);
        if(Input.GetKey(KeyCode.D))
            gameObject.transform.Translate(-Vector2.left*moveSpeed*Time.deltaTime);

        if (health <= 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D coll)
	{
        if(coll.gameObject.tag == "Enemy_1" || coll.gameObject.tag == "Enemy_2")
        {
            health--;
            gameObject.GetComponent <Renderer> ().material.color = new Color(240/255f, 125/255f, 125/255f);
            StartCoroutine (return_color());
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
	{
        if(coll.tag == "E_Bullet") 
		{
            health--;
            gameObject.GetComponent <Renderer> ().material.color = new Color(240/255f, 125/255f, 125/255f);
            StartCoroutine (return_color());
		}
	}


    IEnumerator return_color () {
        yield return new WaitForSeconds (0.5f);
        gameObject.GetComponent <Renderer> ().material.color = Color.white;
    }
}
