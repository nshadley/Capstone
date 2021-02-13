using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    protected bool puzzle1 = false;
    [SerializeField]
    protected bool puzzle2 = false;

    public enum RoomDirection { North, East, South, West};
    public RoomDirection RoomDir;

    protected bool doorUp = false;
    protected bool doorDown = false;
    protected bool doorRight = false;
    protected bool doorLeft = false;


    void Awake()
    {
        Door door = GetComponentInChildren<Door>();
        if (door.doorDir == Door.DoorDirection.Up)
            doorUp = true;
        if (door.doorDir == Door.DoorDirection.Down)
            doorDown = true;
        if (door.doorDir == Door.DoorDirection.Left)
            doorLeft = true;
        if (door.doorDir == Door.DoorDirection.Right)
            doorRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
