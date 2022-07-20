using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//враг просто движется за игроком
public class Enemy_1 : MonoBehaviour
{
    float enemySpeed = 3f;
    Transform target; //параметр объекта игрока
    public float health = 5f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Player pers = GameObject.Find("pers").GetComponent<Player>();
            pers.score += 10; //за убийство игрока начисляется 10 условных единиц

            Destroy(gameObject);
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed*Time.deltaTime);
        
    }


    void OnTriggerEnter2D(Collider2D coll)
	{
        switch (coll.tag)
        {
            case "Bullet":
                health--;
                break;
		}
	}
}
