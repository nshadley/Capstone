using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool connected = false;
    public enum DoorDirection {Up, Down, Left, Right};
    public DoorDirection doorDir;
    public Room parentRoom;

    void Awake()
    {
        parentRoom = GetComponentInParent<Room>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsDoorHorizontal()
    {
        if (parentRoom.RoomDir == Room.RoomDirection.East || parentRoom.RoomDir == Room.RoomDirection.West)
        {
            if(doorDir == DoorDirection.Down || doorDir == DoorDirection.Up)
            {
                return false;
            }
            if(doorDir == DoorDirection.Left || doorDir == DoorDirection.Right)
            {
                return true;
            }
        }
        if (parentRoom.RoomDir == Room.RoomDirection.North || parentRoom.RoomDir == Room.RoomDirection.South)
        {
            if (doorDir == DoorDirection.Down || doorDir == DoorDirection.Up)
            {
                return true;
            }
            if (doorDir == DoorDirection.Left || doorDir == DoorDirection.Right)
            {
                return false;
            }
        }

        return false;
    }
}
