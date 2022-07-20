using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//быстрая версия врага, предследующего объект игрока
//перед преследованием он 2 секунды стоит на месте
public class Enemy_3 : MonoBehaviour
{
    float enemySpeed = 4f;
    Transform target;
    public float health = 5f;
    bool activated = false;
    float curTimeout = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!activated)
        {
            if(gameObject.activeInHierarchy){
            curTimeout+=Time.deltaTime;
            if(curTimeout > 2f)
            {
                activated = true;
            }
            }
            return;
        }
        if (health <= 0)
        {
            Player pers = GameObject.Find("pers").GetComponent<Player>();
            pers.score += 10; //+10 условных единиц за убийство
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
