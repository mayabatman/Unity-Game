using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//рандомная генерация комнат

public class RoomPlacer : MonoBehaviour
{
    public Room [] RoomPrefabs; //массив префабов комнат
    int RoomNumber;//номер комнаты в массиве префабов

    public Room StartingRoom; //стартовая комната

    public Room [,] spawnedRooms; //массив созданных комнат

    public bool [] defeatedRooms; //массив информации о победах в комнатах
    public int n; //целое число для работы с массивом defeatedRooms

    bool BossSpawned; //информация о том была ли сгенерированна комната с боссом
    
    // Start is called before the first frame update
    void Start()
    {
        spawnedRooms = new Room[11, 11];
        spawnedRooms[5, 5] = StartingRoom; //стартовая комната в середине
        StartingRoom.RoomPos = new Vector2Int(5,5);
        BossSpawned = false;
        n = 0;

        for (int i = 0; i < 12; i++) //создаем примерно 12 комнат
        {
            RoomNumber = Random.Range(0, RoomPrefabs.Length); //выбираем рандомную комнату
            while(BossSpawned && RoomNumber == 0) //если комната босса уже была сгенерированна, нельзя допустить повтора
                RoomNumber = Random.Range(0, RoomPrefabs.Length);
            PlaceOneRoom(RoomNumber);
        }
        while (!BossSpawned) //если оказалось, что комната босса так и не была создана, создадим насильно
        {
            PlaceOneRoom(0);
        }
        for(int i = 0; i < defeatedRooms.Length; i++) //ещё нигде не победили
        {
            defeatedRooms[i] = false;
        }
        n = 0;

        //подсвечиваем лампочки в стартовой комнате
        StartingRoom.CloseDoors();
        StartingRoom.OpenDoors();
    }


    void PlaceOneRoom(int RoomNumber)
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>(); //список вакантных мест
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                //если комната не занята идем к следующей
                if (spawnedRooms[x, y] == null) continue;

                //В противном случае, если места вокруг неё пусты, добавляем эти места в список вакантных
                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;

                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));
            }
        }

        Room newRoom = Instantiate(RoomPrefabs[RoomNumber]); //создали комнату

        int limit = 500;
        while (limit-- > 0)
        {
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count)); //выбираем любое подходящее место
            if (ConnectToSomething(newRoom, position)) //если комнаты были соединены
            {
                newRoom.transform.position = new Vector2((position.x - 5)*19.6f, (position.y - 5)*11f);
                spawnedRooms[position.x, position.y] = newRoom;
                newRoom.RoomPos = position;
                
                if (RoomNumber == 0)
                {
                    BossSpawned = true;
                    newRoom.GetComponent<RoomBoss>().myPos = new Vector2Int(position.x, position.y);
                    return;
                }

                defeatedRooms = new bool[++n]; //увеличиваем массив информации о комнатах и победах в них
                return;
            }
        }

        //в случае если выход из цикла был совершен, значит комната никуда не присоединилась
        Destroy(newRoom.gameObject);

    }

    private bool ConnectToSomething(Room room, Vector2Int p) //присоединяем к другим комнатам
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorR != null && p.y < maxY && spawnedRooms[p.x+1, p.y]?.DoorL != null) neighbours.Add(Vector2Int.right);
        if (room.DoorL != null && p.y > 0 && spawnedRooms[p.x-1, p.y]?.DoorR != null) neighbours.Add(Vector2Int.left);
        if (room.DoorU != null && p.x < maxX && spawnedRooms[p.x, p.y+1]?.DoorD != null) neighbours.Add(Vector2Int.up);
        if (room.DoorD != null && p.x > 0 && spawnedRooms[p.x, p.y-1]?.DoorU != null) neighbours.Add(Vector2Int.down);

        if (neighbours.Count == 0)  //если нет соседей
        {
            return false;
        }

        Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
        int rx = p.x + selectedDirection.x;
        int ry = p.y + selectedDirection.y;
        Room selectedRoom = spawnedRooms[rx,ry];

        //открываем нужную дверь у соседей и в самой комнате
        if(selectedDirection == Vector2Int.up)
        {
            room.DoorU.HasHeighbour = true;
            selectedRoom.DoorD.HasHeighbour = true;
        }
        else if (selectedDirection == Vector2Int.down)
        {
            room.DoorD.HasHeighbour = true;
            selectedRoom.DoorU.HasHeighbour = true;
        }
        else if (selectedDirection == Vector2Int.right)
        {
            room.DoorR.HasHeighbour = true;
            selectedRoom.DoorL.HasHeighbour = true;
        }
        else if (selectedDirection == Vector2Int.left)
        {
            room.DoorL.HasHeighbour = true;
            selectedRoom.DoorR.HasHeighbour = true;
        }

        return true;
    }




}


