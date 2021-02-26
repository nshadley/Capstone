﻿using System.Collections;
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

        RoomDir = GetRandomEnum<RoomDirection>();

        if(RoomDir == RoomDirection.East)
        {
            gameObject.transform.Rotate(0, 90, 0);
        }
        else if(RoomDir == RoomDirection.South)
        {
            gameObject.transform.Rotate(0, 180, 0);
        }
        else if (RoomDir == RoomDirection.West)
        {
            gameObject.transform.Rotate(0, 270, 0);
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

    public void RotateRoom()
    {
        int random = Random.Range(0, 1);
        if(random == 0)
        {
            gameObject.transform.Rotate(0, 90, 0);
        }
        else
        {
            gameObject.transform.Rotate(0, 90, 0);
        }
    }
}
