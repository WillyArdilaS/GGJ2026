using System;
using System.Linq;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    // === Rooms ===
    [SerializeField] private RoomInfo[] rooms;
    private int currentRoomIndex;

    // === Camera ===
    private Camera mainCam;

    // === Properties ===
    public RoomInfo[] Rooms => rooms;
    public int CurrentRoomIndex => currentRoomIndex;

    void Awake()
    {
        mainCam = Camera.main;
    }

    void Start()
    {
        // Sort rooms by x position in ascending order and find the current one
        rooms = rooms.OrderBy(room => room.Position.x).ToArray();
        for (int i = 0; i < rooms.Length; i++)
        {
            rooms[i].gameObject.SetActive(false);
            if(rooms[i].Position.x == mainCam.transform.position.x && rooms[i].Position.y == mainCam.transform.position.y) currentRoomIndex = i;
        }

        rooms[currentRoomIndex].gameObject.SetActive(true);
    }

    public void MoveToPrevious()
    {
       if(currentRoomIndex > 0)
        {
            rooms[currentRoomIndex].gameObject.SetActive(false);
            rooms[currentRoomIndex-1].gameObject.SetActive(true);

            currentRoomIndex--;
            mainCam.transform.position = new(rooms[currentRoomIndex].Position.x, rooms[currentRoomIndex].Position.y, mainCam.transform.position.z);
        }
    }
    
    public void MoveToNext()
    {
        if(currentRoomIndex < rooms.Length - 1)
        {
            rooms[currentRoomIndex].gameObject.SetActive(false);
            rooms[currentRoomIndex+1].gameObject.SetActive(true);

            currentRoomIndex++;
            mainCam.transform.position = new(rooms[currentRoomIndex].Position.x, rooms[currentRoomIndex].Position.y, mainCam.transform.position.z);
        }
    }
}