using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//неподвижный враг стреляет в объект игрока
public class Enemy_2 : MonoBehaviour
{
	public Rigidbody2D bullet; // префаб пули
    float health = 5f;

    Transform target; //параметр объекта игрока

    private float curTimeout;

    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("pers").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Player pers = GameObject.Find("pers").GetComponent<Player>();
            pers.score += 10; //+10 баллов за убийство
            Destroy(gameObject);
        }

        Fire();

    }

    void Fire()
	{
        Vector3 difference = target.position - transform.position;
        float rotateZ = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;

		curTimeout += Time.deltaTime;
        float rnd = Random.Range(110,320)/100f; //частота появления пули рандомная
		if(curTimeout > rnd)
		{
            curTimeout = 0;
			Rigidbody2D clone = Instantiate(bullet, gameObject.transform.position, Quaternion.Euler(0f,0f, -rotateZ)) as Rigidbody2D;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
	{
        if(coll.tag == "Bullet") 
		{
            health--;
		}
	}
}
