using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
	public Rigidbody2D bullet; // префаб нашей пули
    float health = 2f;

    Transform target;
    float speed = 9f;
    Transform ghost;

    public float fireRate = 2f; // скорострельность
    private float curTimeout;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        Fire();

    }

    void Fire()
	{
		curTimeout += Time.deltaTime;
		if(curTimeout > fireRate)
		{
            curTimeout = 0;
			Rigidbody2D clone = Instantiate(bullet, gameObject.transform.position, Quaternion.identity) as Rigidbody2D;
            /*target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            clone.transform.position = Vector2.MoveTowards(gameObject.transform.position, target.position, speed*Time.deltaTime);
            clone.transform.right = gameObject.transform.right;*/
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
	{
        if(coll.tag == "Bullet") 
		{
            health--;
		}
	}


/*
    IEnumerator Fire()
    {
        yield return new WaitForSeconds(2);
        Rigidbody2D clone = Instantiate(bullet, gameObject.transform.position, Quaternion.identity) as Rigidbody2D;
    }*/
}
