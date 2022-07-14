using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_3 : MonoBehaviour
{
    float enemySpeed = 4f;
    Transform target;
    public float health = 5f;
    bool activated = false;
    float curTimeout = 0;
    //public GameObject HealthBonus;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        yield return new WaitForSeconds(2f);
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
               // StartCoroutine(Wait());
            }
            }
            return;
        }
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

/*    IEnumerator Wait()
    {
        Debug.Log("Waiting");
        yield return new WaitForSeconds (10f);
    }*/


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
