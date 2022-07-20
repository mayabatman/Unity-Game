using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//данный код осуществляет движение пули не по прямой, а с предследованием игрока, 
//однако она очень недолго существует, поэтому не слишком повышает сложность

public class boss_bullet : MonoBehaviour
{
    Transform target; //параметр объекта игрока
    float speed = 9f; //скорость пули

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.6f); //пуля будет уничтожена через 0,6 секунды
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed*Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D coll)
	{
		if(!coll.isTrigger && !(coll.tag == "Enemy_2") && !(coll.tag == "Enemy_1")) // чтобы пуля не реагировала на других существ и триггеры, если такие будут
		{
			switch(coll.tag)
			{
			case "Player":
				break;
            case "Abyss": // в случае соприкосновения с пропастью
                return;
                break;
			}

			Destroy(gameObject);
		}
	}
}
