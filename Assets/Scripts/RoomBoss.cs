using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBoss : MonoBehaviour
{
    public door Door; //верхняя дверь комнаты босса
    door otherDoor; //нижняя дверь примыкающая к двери в комнату босса
    public GameObject statusU;//лампочка комнаты босса

    RoomPlacer RP; //объект класса RoomPlacer
    public Vector2Int myPos; //позиция комнаты босса в массиве комнат
    bool opened; // чтобы не открывать двери раньше времени

    public GameObject nextStatus; //лампочка двери на следующий уровень
    public door newLevelDoor; //дверь в следующий уровень
    public door_trigger NextLevel;

    // Start is called before the first frame update
    void Start()
    {
        RP = GameObject.Find("Main Camera").GetComponent<RoomPlacer>();
        otherDoor = RP.spawnedRooms[myPos.x,myPos.y+1].DoorD;
        opened = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!RP.defeatedRooms[RP.defeatedRooms.Length-1]) //пока не победили всех монстров в лабиринте дверь не откроется
        {
            Door.HasEnemy = true;
            Door.gameObject.SetActive(true);
            otherDoor.HasEnemy = true;
            otherDoor.gameObject.SetActive(true);
            newLevelDoor.gameObject.SetActive(true);
            NextLevel.gameObject.SetActive(false);
            statusU.GetComponent <Renderer> ().material.color = Color.red;
            RP.spawnedRooms[myPos.x,myPos.y+1].statusD.GetComponent <Renderer> ().material.color = Color.black;
            //дверь подсвечивается черным, чтобы было ясно, что это комната босса
        }
        if(RP.defeatedRooms[RP.defeatedRooms.Length-1]&&!opened) //если все комнаты пройдены, но двери ещё не октрыты, то надо бы открыть
        {
            OpenDoors();
            opened = true;
        }
        if(gameObject.GetComponent<Room>().EnDefeated) //когда победили босса
        {
            newLevelDoor.gameObject.SetActive(false);
            nextStatus.GetComponent <Renderer> ().material.color = Color.yellow; //желтенькая подсветка лампочки над дверью в следующий уровень
            NextLevel.gameObject.SetActive(true);

        }
    }

    void OpenDoors()
    {
        Door.gameObject.SetActive(false);
        otherDoor.gameObject.SetActive(false);
        statusU.GetComponent <Renderer> ().material.color = Color.green;
        RP.spawnedRooms[myPos.x,myPos.y+1].statusD.GetComponent <Renderer> ().material.color = new Color(20/255f,85/255f,25/255f);
    }
}
