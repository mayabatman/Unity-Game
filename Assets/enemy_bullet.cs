using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bullet : MonoBehaviour
{
    Transform target;
    float speed = 9f;
    Vector3 ghost;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        ghost = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, ghost, speed*Time.deltaTime);
        if (transform.position == ghost)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
	{
		if(!coll.isTrigger && !(coll.tag == "Enemy_2")) // чтобы пуля не реагировала на триггер
		{
			switch(coll.tag)
			{
			case "Player":
				break;
			}

			Destroy(gameObject);
		}
	}
}
