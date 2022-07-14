using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class Bullet : MonoBehaviour {

	
	void Start()
	{
		GameObject.Find("pers").GetComponent<Player>().bullets++;
		// уничтожить объект по истечению указанного времени (секунд), если пуля никуда не попала
		Destroy(gameObject, 2);
	}
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if(!coll.isTrigger && !(coll.tag == "Player")) // чтобы пуля не реагировала на триггер
		{
			switch(coll.tag)
			{
			case "Enemy_1":
				//Destroy(coll.gameObject);
				break;
			case "Enemy_2":
				// что-то еще...
				break;
			case "Abyss":
                return;
                break;
			}

			Destroy(gameObject);
		}
	}
}
