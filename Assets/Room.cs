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

    public bool EnDefeated = false;
    public bool PlayerInside = false;

    GameObject MainCam;

    float cam_step_horizontal = 23.5f;
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
                ActEnemys();
            }
        }

        if(!EnDefeated && PlayerInside)
        {
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
        CloseDoors();
    }

    void EnActive()
    {
        GameObject[] enemy_1;
        enemy_1 = GameObject.FindGameObjectsWithTag("Enemy_1");
        GameObject[] enemy_2;
        enemy_2 = GameObject.FindGameObjectsWithTag("Enemy_2");

        if (enemy_1.Length == 0 && enemy_2.Length == 0)
        {
            EnDefeated = true;
            print("нет врагов");
            OpenDoors();
        }
        else 
            CloseDoors();

        /*for (int i = 0; i < enemy_1.Length - 1; i++)
        {
            enemy_1[i].SetActive(true);
            CloseDoors();
        }

        for (int i = 0; i < enemy_2.Length - 1; i++)
        {
            enemy_2[i].SetActive(true);
            CloseDoors();
        }*/
    }
    

    void CloseDoors()
    {
        print("закрываем двери");
        DoorD.gameObject.SetActive(true);
        DoorR.gameObject.SetActive(true);
        DoorL.gameObject.SetActive(true);
        DoorU.gameObject.SetActive(true);
    }

    void OpenDoors()
    {
        print("открываем двери");
        if (DoorD.HasHeighbour)
        {
            DoorD.gameObject.SetActive(false);
        }
        if (DoorR.HasHeighbour)
        {
            DoorR.gameObject.SetActive(false);
        }
        if (DoorL.HasHeighbour)
        {
            DoorL.gameObject.SetActive(false);
        }
        if (DoorU.HasHeighbour)
        {
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
