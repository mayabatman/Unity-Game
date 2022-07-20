using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Rigidbody2D bullet1; //пуля первой фазы
    public Rigidbody2D bullet2; //пуля второй фазы
    float speed = 2f; //скорость босса по умолчанию
    Transform target; //параметр объекта игрока
    public Slider HealthBar; //слайдер здоровья босса
    //public float fireRate = 2f; // скорострельность
    private float curTimeout; //переменная для регулирования частоты выстрелов

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.maxValue = 100;
        HealthBar.value = 100;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HealthBar.value <= 0)
        {
            Player pers = GameObject.Find("pers").GetComponent<Player>();
            pers.score += 1000; //за победу над боссом начисляется 1000 условных единиц
            Destroy(HealthBar.gameObject);
            Destroy(gameObject);
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed*Time.deltaTime);

        if(HealthBar.value > 30) //первая фаза
            Fire1();
        else                    // вторая фаза
            Fire2();
    }

    void Fire1() //пуля появляется с разворотом в сторону объекта игрока
	{
		Vector3 difference = target.position - transform.position;
        float rotateZ = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;

        curTimeout += Time.deltaTime;

        float rnd = Random.Range(80,250)/100f; //рандомные промежутки времени между выстрелами в пределах диапазона
		if(curTimeout > rnd)
		{
            curTimeout = 0;
			Rigidbody2D clone = Instantiate(bullet1, gameObject.transform.position, Quaternion.Euler(0f,0f, -rotateZ)) as Rigidbody2D;
        }
    }

    void Fire2() //пуля появляется и перследует объект игрока
    {
        curTimeout +=Time.deltaTime;
        
        float rnd = Random.Range(50, 230)/100f;//рандомные промежутки времени между выстрелами в пределах диапазона
        if(curTimeout > rnd)
        {
            curTimeout = 0;
			Rigidbody2D clone = Instantiate(bullet2, gameObject.transform.position, Quaternion.identity) as Rigidbody2D;
        }
    }

    void OnTriggerEnter2D(Collider2D coll) //когда игрок попадает в цель
	{
        switch (coll.tag)
        {
            case "Bullet":
                HealthBar.value--;
                if (HealthBar.value == 30)
                {
                    changecolor();
                    speed++;
                }
                break;
		}
	}

    void changecolor() //изменение подсветки босса
    {
        gameObject.GetComponent <Renderer> ().material.color = new Color(255/255f, 170/255f, 170/255f);
    }
}
