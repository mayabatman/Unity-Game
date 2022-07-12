using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomPlacer : MonoBehaviour
{
    public Room [] RoomPrefabs;
    public Room StartingRoom;

    public Room [,] spawnedRooms;

    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        spawnedRooms = new Room[11, 11];
        spawnedRooms[5, 5] = StartingRoom;
        StartingRoom.RoomPos = new Vector2Int(5,5);

        for (int i = 0; i < 12; i++)
        {
            // Это вот просто убрать чтобы подземелье генерировалось мгновенно на старте
            yield return new WaitForSecondsRealtime(0.5f);

            PlaceOneRoom();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaceOneRoom()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
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

        Room newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);

        int limit = 500;
        while (limit-- > 0)
        {
            // Эту строчку можно заменить на выбор положения комнаты с учётом того насколько он далеко/близко от центра,
            // или сколько у него соседей, чтобы генерировать более плотные, или наоборот, растянутые данжи
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));

            if (ConnectToSomething(newRoom, position)) //если комнаты были соединены
            {
                newRoom.transform.position = new Vector2((position.x - 5)*23.5f, (position.y - 5)*11f);
                spawnedRooms[position.x, position.y] = newRoom;
                newRoom.RoomPos = position;
                return;
            }
        }


        //в случае если выход из цикла был совершен, значит комната никуда не присоединилась
        Destroy(newRoom.gameObject);

    }

    private bool ConnectToSomething(Room room, Vector2Int p)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;

        List<Vector2Int> neighbours = new List<Vector2Int>();

        if (room.DoorR != null && p.y < maxY && spawnedRooms[p.x, p.y + 1]?.DoorL != null) neighbours.Add(Vector2Int.up);
        if (room.DoorL != null && p.y > 0 && spawnedRooms[p.x, p.y - 1]?.DoorR != null) neighbours.Add(Vector2Int.down);
        if (room.DoorU != null && p.x < maxX && spawnedRooms[p.x + 1, p.y]?.DoorD != null) neighbours.Add(Vector2Int.right);
        if (room.DoorD != null && p.x > 0 && spawnedRooms[p.x - 1, p.y]?.DoorU != null) neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) 
        {
            print(p.x+";"+p.y+" нет соседей");
            return false;
        }

        Vector2Int selectedDirection = neighbours[Random.Range(0, neighbours.Count)];
        int rx = p.x + selectedDirection.x;
        int ry = p.y + selectedDirection.y;
        Room selectedRoom = spawnedRooms[rx,ry];
        print("Подключаюсь к "+rx+";"+ry);

        if(selectedDirection == Vector2Int.up)
        {
            print("Подключаюсь к верхней");
            /*room.DoorU.gameObject.SetActive(false);
            selectedRoom.DoorD.gameObject.SetActive(false);
*/
            room.DoorU.HasHeighbour = true;
            selectedRoom.DoorD.HasHeighbour = true;
        }
        else if (selectedDirection == Vector2Int.down)
        {
            print("Подключаюсь к нижней");
            /*room.DoorD.gameObject.SetActive(false);
            selectedRoom.DoorU.gameObject.SetActive(false);
*/
            room.DoorD.HasHeighbour = true;
            selectedRoom.DoorU.HasHeighbour = true;
        }
        else if (selectedDirection == Vector2Int.right)
        {
            print("Подключаюсь к правой");
            /*room.DoorR.gameObject.SetActive(false);
            selectedRoom.DoorL.gameObject.SetActive(false);
*/
            room.DoorR.HasHeighbour = true;
            selectedRoom.DoorL.HasHeighbour = true;
        }
        else if (selectedDirection == Vector2Int.left)
        {
            print("Подключаюсь к левой");
            /*room.DoorL.gameObject.SetActive(false);
            selectedRoom.DoorR.gameObject.SetActive(false);
*/
            room.DoorL.HasHeighbour = true;
            selectedRoom.DoorR.HasHeighbour = true;
        }

        return true;
    }




}


