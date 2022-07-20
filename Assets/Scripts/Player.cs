using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public float range = 5f, moveSpeed = 5f;
    public GameObject cam; //объект камеры, которая двигается за игроком в другую комнату
    public bool InDoor; //находится ли игрок в дверях
    public Slider HealthBar;
    public int score = 0;
    public TMP_Text scoreText; //выведение данных об условных очках
    

    // Start is called before the first frame update
    void Start()
    {
        HealthBar.maxValue = 30;
        HealthBar.value = 30;
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();

//самое простое управление движения

        if(Input.GetKey(KeyCode.W))
            gameObject.transform.Translate(Vector2.up*moveSpeed*Time.deltaTime);
        if(Input.GetKey(KeyCode.S))
            gameObject.transform.Translate(-Vector2.up*moveSpeed*Time.deltaTime);
        if(Input.GetKey(KeyCode.A))
            gameObject.transform.Translate(Vector2.left*moveSpeed*Time.deltaTime);
        if(Input.GetKey(KeyCode.D))
            gameObject.transform.Translate(-Vector2.left*moveSpeed*Time.deltaTime);
        if(Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene("menu", LoadSceneMode.Single);
        if (HealthBar.value <= 0){ //при погибели сохраняется рекорд и выводится главное меню
            Destroy(gameObject);
            Records(score);
            PlayerPrefs.Save();
            SceneManager.LoadScene("menu", LoadSceneMode.Single);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
	{
        if(coll.gameObject.tag == "Enemy_1" || coll.gameObject.tag == "Enemy_2" || coll.gameObject.tag == "Danger" || coll.gameObject.tag == "Boss")
        {
            HealthBar.value--;
            gameObject.GetComponent <Renderer> ().material.color = new Color(240/255f, 125/255f, 125/255f); //при поражении пулей подсветка игрока меняется
            StartCoroutine (return_color());
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
	{
        switch (coll.tag) 
		{
        case "E_Bullet": //при встече с вражеской пулей
            HealthBar.value--;
            gameObject.GetComponent <Renderer> ().material.color = new Color(240/255f, 125/255f, 125/255f);
            StartCoroutine (return_color());
            break;
        case "trigger_door": // в дверном преме
            coll.GetComponent<door_trigger>().act = true;
            break;
        case "Health":
            HealthBar.value++;
            Destroy(coll.gameObject);
            break;
        case "NextLevel": //при переходе на новый уровень
            Records(score);
            PlayerPrefs.Save(); //рекорд сохранили
            SceneManager.LoadScene("next", LoadSceneMode.Single);
            break;
		}
	}

//отслеживаем, находится ли игрок в дверях

    void OnTriggerStay2D(Collider2D coll)
    {
        switch (coll.tag)
        {
        case "trigger_door":
            InDoor = true;
            break;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        switch (coll.tag)
        {
        case "trigger_door":
            InDoor = false;
            break;
        }
    }

    public void Records(int sc) // обновление списка рекордов
    {
        for (int i = 0; i < 5; i++)
        {
            int rec = PlayerPrefs.GetInt((i+1).ToString());
            if (sc > rec){
                PlayerPrefs.SetInt((i+1).ToString(), sc);
                sc = rec;
            }
        }
    }


    IEnumerator return_color () {
        yield return new WaitForSeconds (0.5f);
        gameObject.GetComponent <Renderer> ().material.color = Color.white;
    }
}
