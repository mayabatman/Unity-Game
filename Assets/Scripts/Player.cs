using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public float range = 5f, moveSpeed = 5f;//, health = 5f;
    public GameObject cam;
    public int bullets = 0;
    public bool InDoor;
    public Slider HealthBar;
    public int score = 0;
    public TMP_Text scoreText;
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("bullets"));
        HealthBar.maxValue = 20;
        HealthBar.value = 19;
    }
    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();

        if(Input.GetKey(KeyCode.W))
            gameObject.transform.Translate(Vector2.up*moveSpeed*Time.deltaTime);
        if(Input.GetKey(KeyCode.S))
            gameObject.transform.Translate(-Vector2.up*moveSpeed*Time.deltaTime);
        if(Input.GetKey(KeyCode.A))
            gameObject.transform.Translate(Vector2.left*moveSpeed*Time.deltaTime);
        if(Input.GetKey(KeyCode.D))
            gameObject.transform.Translate(-Vector2.left*moveSpeed*Time.deltaTime);

        if (HealthBar.value <= 0){
            Destroy(gameObject);
            Records(score);
            //PlayerPrefs.SetInt("bullets", bullets);
            PlayerPrefs.Save();
            SceneManager.LoadScene("menu", LoadSceneMode.Single);

        }
    }

    void OnCollisionEnter2D(Collision2D coll)
	{
        if(coll.gameObject.tag == "Enemy_1" || coll.gameObject.tag == "Enemy_2" || coll.gameObject.tag == "Danger")
        {
            HealthBar.value--;
            gameObject.GetComponent <Renderer> ().material.color = new Color(240/255f, 125/255f, 125/255f);
            StartCoroutine (return_color());
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
	{
        switch (coll.tag) 
		{
        case "E_Bullet":
            HealthBar.value--;
            gameObject.GetComponent <Renderer> ().material.color = new Color(240/255f, 125/255f, 125/255f);
            StartCoroutine (return_color());
            break;
        case "trigger_door":
            //InDoor = true;
           // cam.transform.Translate(Vector2.right*cam_step);
            print("Добро пожаловать в комнату");
            coll.GetComponent<door_trigger>().act = true;
            if (coll.GetComponent<door_trigger>().act)
                print ("active now");
            break;
        case "Health":
            HealthBar.value++;
            Debug.Log("Забираю здровье");
            Destroy(coll.gameObject);
            break;
		}
	}

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
            //coll.GetComponent<door_trigger>().act = false;
            break;
        }
    }

    void Records(int sc)
    {
        for (int i = 0; i < 5; i++)
        {
            int rec = PlayerPrefs.GetInt((i+1).ToString());
            if (sc > rec){
                Debug.Log(sc+" > "+rec);
                PlayerPrefs.SetInt((i+1).ToString(), sc);
                sc = rec;
            }
            else Debug.Log(sc+" < "+rec);

        }
    }


    IEnumerator return_color () {
        yield return new WaitForSeconds (0.5f);
        gameObject.GetComponent <Renderer> ().material.color = Color.white;
    }
}
