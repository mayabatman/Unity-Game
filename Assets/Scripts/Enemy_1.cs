using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : MonoBehaviour
{
    float enemySpeed = 3f;
    Transform target;
    public float health = 5f;
    public GameObject HealthBonus;

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
            pers.score = pers.score + 10;
            //int bonus = Random.Range(0,10);
            //Debug.Log("ВАУ ЭТО "+bonus);
            //if (bonus == 4)
            
                //GameObject drop = Instantiate(HealthBonus, gameObject.transform);
                //drop.transform.position = gameObject.transform.position;
            
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
