using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public door DoorU;
    public door DoorR;
    public door DoorD;
    public door DoorL;

    public door_trigger dtU;
    public door_trigger dtR;
    public door_trigger dtD;
    public door_trigger dtL;

    public Vector2Int RoomPos;

    public GameObject[] enemys;
    public GameObject Player;

    public bool EnDefeated = false;
    public bool PlayerInside = false;

    GameObject MainCam;

    float cam_step_horizontal = 19.6f;
    float cam_step_vertical = 11f;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "StartRoom")
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
            if (dtL.act)
            {
                print (RoomPos.x+","+RoomPos.y);
                PlayerInside = true;
                Vector2Int lastPos = new Vector2Int(RoomPos.x-1, RoomPos.y);
                print ("lastPos = "+lastPos.x+","+lastPos.y);
                MainCam.GetComponent<RoomPlacer>().spawnedRooms[RoomPos.x-1, RoomPos.y].deactDT();
                MainCam.GetComponent<RoomPlacer>().spawnedRooms[RoomPos.x-1, RoomPos.y].PlayerInside = false; //тут координаты противоположные, потому что это изначально чужой код
                MainCam.transform.Translate(Vector2.right*cam_step_horizontal);
                Player.transform.Translate(Vector2.right*2f);
                ActEnemys();
            }
            if (dtR.act)
            {
                print (RoomPos.x+","+RoomPos.y);
                PlayerInside = true;
                Vector2Int lastPos = new Vector2Int(RoomPos.x+1, RoomPos.y);
                print ("lastPos = "+lastPos.x+","+lastPos.y);
                MainCam.GetComponent<RoomPlacer>().spawnedRooms[RoomPos.x+1, RoomPos.y].deactDT();
                MainCam.GetComponent<RoomPlacer>().spawnedRooms[RoomPos.x+1, RoomPos.y].PlayerInside = false; //тут координаты противоположные, потому что это изначально чужой код
                MainCam.transform.Translate(-Vector2.right*cam_step_horizontal);
                Player.transform.Translate(-Vector2.right*2f);
                ActEnemys();
            }
            if (dtU.act)
            {
                print (RoomPos.x+","+RoomPos.y);
                PlayerInside = true;
                Vector2Int lastPos = new Vector2Int(RoomPos.x, RoomPos.y+1);
                print ("lastPos = "+lastPos.x+","+lastPos.y);
                MainCam.GetComponent<RoomPlacer>().spawnedRooms[RoomPos.x, RoomPos.y+1].deactDT();
                MainCam.GetComponent<RoomPlacer>().spawnedRooms[RoomPos.x, RoomPos.y+1].PlayerInside = false; //тут координаты противоположные, потому что это изначально чужой код
                MainCam.transform.Translate(-Vector2.up*cam_step_vertical);
                Player.transform.Translate(-Vector2.up*2f);
                ActEnemys();
            }
            if (dtD.act)
            {
                print (RoomPos.x+","+RoomPos.y);
                PlayerInside = true;
                Vector2Int lastPos = new Vector2Int(RoomPos.x, RoomPos.y-1);
                print ("lastPos = "+lastPos.x+","+lastPos.y);
                MainCam.GetComponent<RoomPlacer>().spawnedRooms[RoomPos.x, RoomPos.y-1].deactDT();
                MainCam.GetComponent<RoomPlacer>().spawnedRooms[RoomPos.x, RoomPos.y-1].PlayerInside = false; //тут координаты противоположные, потому что это изначально чужой код
                MainCam.transform.Translate(Vector2.up*cam_step_vertical);
                Player.transform.Translate(Vector2.up*2f);
                ActEnemys();
            }
        }

        if(!EnDefeated && PlayerInside)
        {
            if (!GameObject.Find("pers").GetComponent<Player>().InDoor)
                CloseDoors();
            EnActive();
        }
    }

    void deactDT()
    {
        dtU.act = false;
        dtR.act = false;
        dtD.act = false;
        dtL.act = false;
    }

    void ActEnemys()
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(true);
        }
        
    }

    void EnActive()
    {
        GameObject[] enemy_1;
        enemy_1 = GameObject.FindGameObjectsWithTag("Enemy_1");
        GameObject[] enemy_2;
        enemy_2 = GameObject.FindGameObjectsWithTag("Enemy_2");

        Debug.Log("1 "+enemy_1.Length);
        Debug.Log("2. "+enemy_2.Length);

        if (enemy_1.Length == 0 && enemy_2.Length == 0)
        {
            EnDefeated = true;
            print("нет врагов");
            Player.GetComponent<Player>().score = Player.GetComponent<Player>().score+100;
            OpenDoors();
        }
        else
        { 
            if (!GameObject.Find("pers").GetComponent<Player>().InDoor)
                CloseDoors();
        }
    }
    

    void CloseDoors()
    {
        //print("закрываем двери");
        if (DoorD != null)
        DoorD.HasEnemy = true;
        if (DoorR != null)
        DoorR.HasEnemy = true;
        if (DoorL != null)
        DoorL.HasEnemy = true;
        if (DoorU != null)
        DoorU.HasEnemy = true;
        if (DoorD != null)
        DoorD.gameObject.SetActive(true);
        if (DoorR != null)
        DoorR.gameObject.SetActive(true);
        if (DoorL != null)
        DoorL.gameObject.SetActive(true);
        if (DoorU != null)
        DoorU.gameObject.SetActive(true);
    }

    void OpenDoors()
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
            if (DoorD.HasHeighbour)
                DoorD.gameObject.SetActive(false);
        }
        if (DoorR != null)
        {
            if (DoorR.HasHeighbour)
            DoorR.gameObject.SetActive(false);
        }
        if (DoorL != null)
        {
            if (DoorL.HasHeighbour)
            DoorL.gameObject.SetActive(false);
        }
        if (DoorU != null)
        {
            if (DoorU.HasHeighbour)
            DoorU.gameObject.SetActive(false);
        }
    }

    void RoomsStatus()
    {
        for (int x = 0; x < MainCam.GetComponent<RoomPlacer>().spawnedRooms.GetLength(0); x++ )
        {
            for (int y = 0; y < MainCam.GetComponent<RoomPlacer>().spawnedRooms.GetLength(1); y++ )
            {
                Room temp = MainCam.GetComponent<RoomPlacer>().spawnedRooms[x,y];
                if (temp != null)
                {
                    print (temp.RoomPos.x+","+temp.RoomPos.y);
                    if (temp.PlayerInside)
                        print(" занята");
                    else
                        print (" свободна");
                }
                
            }
        }
    }
}
