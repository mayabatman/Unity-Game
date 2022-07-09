using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    float enemySpeed = 2f;
    Transform target;
    public float health = 2f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            Destroy(gameObject);
        transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed*Time.deltaTime);
        
    }


    void OnTriggerEnter2D(Collider2D coll)
	{
        switch (coll.tag)
        {
            case "Bullet":
                health--;
                break;
            case "E_Bullet":
                health--;
                break;
		}
	}
}
