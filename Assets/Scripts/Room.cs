using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    //объекты дверей
    public door DoorU;
    public door DoorR;
    public door DoorD;
    public door DoorL;

    //объекты триггеров, которые активируются при входе игрока
    public door_trigger dtU;
    public door_trigger dtR;
    public door_trigger dtD;
    public door_trigger dtL;
    
    //объекты лампочек над дверями
    public GameObject statusU;
    public GameObject statusR;
    public GameObject statusD;
    public GameObject statusL;

    //позиция комнаты
    public Vector2Int RoomPos;

    public GameObject[] enemys; //враги в комнате
    public GameObject Player; //объект игрока

    public bool EnDefeated = false; //повержены ли враги
    public bool PlayerInside = false; //находится ли игрок внутри

    GameObject MainCam; //объект камеры (для обращения к классам)

    float cam_step_horizontal = 19.6f; //шаг камеры по горизонту
    float cam_step_vertical = 11f; //шаг камеры по вертикали

    public Slider HealthBarBoss; //HP игрока

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "StartRoom") // в стартовой комнате нет врагов
        {    
            PlayerInside = true;
            EnDefeated = true;
        }
        MainCam = GameObject.Find("Main Camera");
        Player = GameObject.Find("pers");
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerInside)
        {
            //все условные операторы ниже могут сработать только если игрок зашел внутрь
            //поэтому они меняют статус комнаты на то, что игрок внутри

            if (dtL.act)
            {
                print (RoomPos.x+","+RoomPos.y);
                PlayerInside = true;
                Vector2Int lastPos = new Vector2Int(RoomPos.x-1, RoomPos.y);
                MainCam.GetComponent<RoomPlacer>().spawnedRooms[RoomPos.x-1, RoomPos.y].deactDT(); 
                MainCam.GetComponent<RoomPlacer>().spawnedRooms[RoomPos.x-1, RoomPos.y].PlayerInside = false; 
                MainCam.transform.Translate(Vector2.right*cam_step_horizontal);
                Player.transform.Translate(Vector2.right*2f);
                ActEnemys();
            }
            if (dtR.act)
            {
                print (RoomPos.x+","+RoomPos.y);
                PlayerInside = true;
                Vector2Int lastPos = new Vector2Int(RoomPos.x+1, RoomPos.y);
                MainCam.GetComponent<RoomPlacer>().spawnedRooms[RoomPos.x+1, RoomPos.y].deactDT(); //триггеры в прошкой комнате неактивны
                MainCam.GetComponent<RoomPlacer>().spawnedRooms[RoomPos.x+1, RoomPos.y].PlayerInside = false;
                MainCam.transform.Translate(-Vector2.right*cam_step_horizontal);
                Player.transform.Translate(-Vector2.right*2f);
                ActEnemys();
            }
            if (dtU.act)
            {
                print (RoomPos.x+","+RoomPos.y);
                PlayerInside = true;
                Vector2Int lastPos = new Vector2Int(RoomPos.x, RoomPos.y+1);
                MainCam.GetComponent<RoomPlacer>().spawnedRooms[RoomPos.x, RoomPos.y+1].deactDT();//триггеры в прошкой комнате неактивны
                MainCam.GetComponent<RoomPlacer>().spawnedRooms[RoomPos.x, RoomPos.y+1].PlayerInside = false; 
                MainCam.transform.Translate(-Vector2.up*cam_step_vertical);
                Player.transform.Translate(-Vector2.up*2f);
                ActEnemys();
            }
            if (dtD.act)
            {
                print (RoomPos.x+","+RoomPos.y);
                PlayerInside = true;
                Vector2Int lastPos = new Vector2Int(RoomPos.x, RoomPos.y-1);
                MainCam.GetComponent<RoomPlacer>().spawnedRooms[RoomPos.x, RoomPos.y-1].deactDT();//триггеры в прошкой комнате неактивны
                MainCam.GetComponent<RoomPlacer>().spawnedRooms[RoomPos.x, RoomPos.y-1].PlayerInside = false; 
                MainCam.transform.Translate(Vector2.up*cam_step_vertical);
                Player.transform.Translate(Vector2.up*2f);
                ActEnemys();
            }
        }

        if(!EnDefeated && PlayerInside) //если враги ещё неповержены, а игрок уже внутри
        {
            if (!GameObject.Find("pers").GetComponent<Player>().InDoor)
                CloseDoors();
            EnActive();
        }
    }

    void deactDT() //деактивация триггеров комнаты
    {
        dtU.act = false;
        dtR.act = false;
        dtD.act = false;
        dtL.act = false;
    }

    void ActEnemys() //активация врагов
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(true);
        }
        if (HealthBarBoss != null) //в случае если это комната босса активируется и HP слайдер его здоровья
        {
            HealthBarBoss.gameObject.SetActive(true);
        }
    }

    void EnActive()//проверка на то, убиты ли все монстры
    {
        GameObject[] enemy_1;
        enemy_1 = GameObject.FindGameObjectsWithTag("Enemy_1");
        GameObject[] enemy_2;
        enemy_2 = GameObject.FindGameObjectsWithTag("Enemy_2");

        if (enemy_1.Length == 0 && enemy_2.Length == 0)
        {
            EnDefeated = true;
            int n = MainCam.GetComponent<RoomPlacer>().n;
            MainCam.GetComponent<RoomPlacer>().defeatedRooms[n] = true;
            MainCam.GetComponent<RoomPlacer>().n++;
            Player.GetComponent<Player>().score += 100; //за победу в комнате +100 условных очков
            OpenDoors();
        }
        else
        { 
            if (!GameObject.Find("pers").GetComponent<Player>().InDoor)
                CloseDoors();
        }
    }
    

    public void CloseDoors()// закрываем все двери из комнаты
    {
        if (DoorD != null)
        DoorD.HasEnemy = true;
        if (DoorR != null)
        DoorR.HasEnemy = true;
        if (DoorL != null)
        DoorL.HasEnemy = true;
        if (DoorU != null)
        DoorU.HasEnemy = true;
        statusD.GetComponent <Renderer> ().material.color = Color.red;
        statusR.GetComponent <Renderer> ().material.color = Color.red;
        statusL.GetComponent <Renderer> ().material.color = Color.red;
        statusU.GetComponent <Renderer> ().material.color = Color.red;
        if (DoorD != null)
        DoorD.gameObject.SetActive(true);
        if (DoorR != null)
        DoorR.gameObject.SetActive(true);
        if (DoorL != null)
        DoorL.gameObject.SetActive(true);
        if (DoorU != null)
        DoorU.gameObject.SetActive(true);
    }

    public void OpenDoors() //открываем все двери в комнату
    {
        //print("открываем двери");
        if (DoorD != null)
        DoorD.HasEnemy = false;
        if (DoorR != null)
        DoorR.HasEnemy = false;
        if (DoorL != null)
        DoorL.HasEnemy = false;
        if (DoorU != null)
        DoorU.HasEnemy = false;
        
        if (DoorD != null)
        {
            if (DoorD.HasHeighbour){
                DoorD.gameObject.SetActive(false);
                statusD.GetComponent <Renderer> ().material.color = Color.green;
            }
        }
        if (DoorR != null)
        {
            if (DoorR.HasHeighbour){
                DoorR.gameObject.SetActive(false);
                statusR.GetComponent <Renderer> ().material.color = Color.green;
        
            }
        }
        if (DoorL != null)
        {
            if (DoorL.HasHeighbour){
                DoorL.gameObject.SetActive(false);
                statusL.GetComponent <Renderer> ().material.color = Color.green;
            }
        
        }
        if (DoorU != null)
        {
            if (DoorU.HasHeighbour){
                DoorU.gameObject.SetActive(false);
                statusU.GetComponent <Renderer> ().material.color = Color.green;
            }
        }
    }
}
