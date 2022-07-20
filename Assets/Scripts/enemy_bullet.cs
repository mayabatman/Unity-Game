using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bullet : MonoBehaviour
{
    Transform target; //параметр объекта игрока
    float speed = 9f; //скорость пули

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2); //пуля существует 2 сек
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed *Time.deltaTime); //пуля летит прямо (в коде Enemy_2 и Boss перед этим она поворачивается в нужную сторону)
    }

    void OnTriggerEnter2D(Collider2D coll)
	{
		if(!coll.isTrigger && !(coll.tag == "Enemy_2")&& !(coll.tag == "Enemy_1")) // чтобы пуля не реагировала на триггер
		{
			switch(coll.tag)
			{
			case "Player":
				break;
            case "Abyss":// в случае соприкосновения с пропастью
                return;
                break;
			}

			Destroy(gameObject);
		}
	}
}
