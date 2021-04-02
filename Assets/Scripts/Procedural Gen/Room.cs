using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField]
    public bool puzzle1;
    [SerializeField]
    public bool puzzle2;

    public enum RoomDirection { North, East, South, West};
    public RoomDirection RoomDir;

    protected bool doorUp = false;
    protected bool doorDown = false;
    protected bool doorRight = false;
    protected bool doorLeft = false;

    public Door[] doors;


    void Awake()
    {
        doors = GetComponentsInChildren<Door>();

        foreach(Door door in doors)
        {
            if (door.doorDir == Door.DoorDirection.Up)
                doorUp = true;
            if (door.doorDir == Door.DoorDirection.Down)
                doorDown = true;
            if (door.doorDir == Door.DoorDirection.Left)
                doorLeft = true;
            if (door.doorDir == Door.DoorDirection.Right)
                doorRight = true;
        }

        if(gameObject.name != "StartRoom")
        {
            RoomDir = GetRandomEnum<RoomDirection>();

            if (RoomDir == RoomDirection.East)
            {
                gameObject.transform.Rotate(0, 90, 0);
            }
            else if (RoomDir == RoomDirection.South)
            {
                gameObject.transform.Rotate(0, 180, 0);
            }
            else if (RoomDir == RoomDirection.West)
            {
                gameObject.transform.Rotate(0, 270, 0);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }

    public Door ChooseDoor()
    {
        return doors[Random.Range(0, doors.Length - 1)];
    }

    public void RotateRoom(GameObject theRoom)
    {
        int random = Random.Range(0, 1);
        if(random == 0)
        {
            theRoom.transform.Rotate(0, 90, 0);
            rightDirection();
        }
        else
        {
            theRoom.transform.Rotate(0, -90, 0);
            leftDirection();
        }
    }

    public void TurnRoomAround(GameObject theRoom)
    {
        theRoom.transform.Rotate(0, 180, 0);
        rightDirection();
        rightDirection();
    }

    void rightDirection()
    {
        switch (RoomDir)
        {
            case RoomDirection.North:
                RoomDir = RoomDirection.East;
                break;
            case RoomDirection.East:
                RoomDir = RoomDirection.South;
                break;
            case RoomDirection.South:
                RoomDir = RoomDirection.West;
                break;
            case RoomDirection.West:
                RoomDir = RoomDirection.North;
                break;
            default:
                break;
        }
    }

    void leftDirection()
    {
        switch (RoomDir)
        {
            case RoomDirection.North:
                RoomDir = RoomDirection.West;
                break;
            case RoomDirection.West:
                RoomDir = RoomDirection.South;
                break;
            case RoomDirection.South:
                RoomDir = RoomDirection.East;
                break;
            case RoomDirection.East:
                RoomDir = RoomDirection.North;
                break;
            default:
                break;
        }
    }
}
