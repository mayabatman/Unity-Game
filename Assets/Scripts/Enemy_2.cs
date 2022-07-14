using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : MonoBehaviour
{
	public Rigidbody2D bullet; // префаб нашей пули
    float health = 5f;

    Transform target;
    Transform ghost;

    public float fireRate = 2f; // скорострельность
    private float curTimeout;

    //public GameObject HealthBonus;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Player pers = GameObject.Find("pers").GetComponent<Player>();
            pers.score = pers.score + 10;
            int bonus = Random.Range(0,10);
            Debug.Log("ВАУ ЭТО "+bonus);
            if (bonus == 4)
            {
                //GameObject drop = Instantiate(HealthBonus);
                //drop.transform.position = gameObject.transform.position;
            }
            Destroy(gameObject);
        }

        Fire();

    }

    void Fire()
	{
		curTimeout += Time.deltaTime;
        float rnd = Random.Range(110,320)/100f;
        Debug.Log(rnd);
		if(curTimeout > rnd)
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
