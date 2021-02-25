using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    int numberOfRooms = 2;

    [SerializeField]
    List<Room> availableRooms;

    List<Room> pickedRooms;

    Door[] currentDoors;
    Door selectedDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateRooms()
    {
        foreach(Room r in availableRooms)
        {
            if(r.puzzle1)
            {
                pickedRooms.Add(r);
            }
        }
        while(pickedRooms.Count < numberOfRooms)
        {
            bool itsANewRoom = false;
            Room aRoom = availableRooms[Random.Range(0, availableRooms.Count - 1)];
            foreach(Room ro in pickedRooms)
            {
                if(ro != aRoom)
                {
                    itsANewRoom = true;
                }
            }
            if(itsANewRoom)
            {
                pickedRooms.Add(aRoom);
            }
        }


    }

    void PickADoor()
    {
        currentDoors = FindObjectsOfType<Door>();

        selectedDoor = currentDoors[Random.Range(0, currentDoors.Length - 1)];
    }

    void PlaceRoom()
    {
        
    }
}
