using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField]
    int numberOfRooms = 2;

    [SerializeField]
    List<GameObject> availableRooms = new List<GameObject>();

    List<GameObject> pickedRooms = new List<GameObject>();

    Door[] currentDoors;
    Door selectedDoor;

    GameManager gm;

    [SerializeField]
    GameObject wall;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    //Start is called before the first frame update
    void Start()
    {
        GenerateRooms();
        PlaceRooms();
        //CleanUpDoors();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateRooms()
    {
        AddPuzzleRooms();       
        Debug.Assert(pickedRooms.Count > 0, "No puzzle rooms were picked");
        AddRandomRooms();

        //shuffle rooms
        pickedRooms = ShuffleList<GameObject>(pickedRooms);
    }

    private List<E> ShuffleList<E>(List<E> inputList)
    {
        List<E> randomList = new List<E>();

        int randomIndex = 0;
        while (inputList.Count > 0)
        {
            randomIndex = Random.Range(0, inputList.Count);
            randomList.Add(inputList[randomIndex]);
            inputList.RemoveAt(randomIndex);
        }

        return randomList;
    }

    void PickADoor()
    {
        currentDoors = FindObjectsOfType<Door>();

        Door doorCheck = currentDoors[Random.Range(0, currentDoors.Length - 1)];

        while (doorCheck.connected)
        {
            doorCheck = currentDoors[Random.Range(0, currentDoors.Length - 1)];
        }

        selectedDoor = doorCheck;
    }

    void PlaceRooms()
    {
        foreach (GameObject room in pickedRooms)
        {
            PickADoor();

            Room roomScript = room.GetComponent<Room>();

            GameObject newRoom = Instantiate(room, selectedDoor.transform.position, room.transform.rotation);

            Door chosenDoor = newRoom.GetComponent<Room>().ChooseDoor();
            GameObject childDoor = chosenDoor.gameObject;
            GameObject toDoor = selectedDoor.gameObject;

            newRoom.transform.position += toDoor.transform.position;

            if (chosenDoor.IsDoorHorizontal() != selectedDoor.IsDoorHorizontal())
            {
                do
                {
                    roomScript.RotateRoom(newRoom);
                }
                while (childDoor.transform.position != toDoor.transform.position);
            }
            
            if (childDoor.transform.position != toDoor.transform.position)
            {
                roomScript.TurnRoomAround(newRoom);
            }

            childDoor.GetComponent<Door>().connected = true;
            toDoor.GetComponent<Door>().connected = true;
            selectedDoor.connected = true;
            chosenDoor.connected = true;

            //delete doors if they are not locked
            if(!childDoor.GetComponent<LockedDoor>() && !toDoor.GetComponent<LockedDoor>())
            {
                Destroy(childDoor);
                Destroy(toDoor);
            }
            else if(childDoor.GetComponent<LockedDoor>())
            {
                Destroy(toDoor);
            }
            else
            {
                Destroy(childDoor);
            }
        }
    }

    void AddPuzzleRooms()
    {
        foreach (GameObject r in availableRooms)
        {
            Room tempRoom = r.GetComponent<Room>();
            if (tempRoom.puzzle1)
            {
                pickedRooms.Add(r);
            }
        }
    }

    void AddRandomRooms()
    {
        while (pickedRooms.Count < numberOfRooms)
        {
            bool itsANewRoom = false;
            GameObject aRoom = availableRooms[Random.Range(0, availableRooms.Count - 1)];
            foreach (GameObject ro in pickedRooms)
            {
                if (ro != aRoom)
                {
                    itsANewRoom = true;
                }
            }
            if (itsANewRoom)
            {
                pickedRooms.Add(aRoom);
            }

        }
    }

    void CleanUpDoors()
    {
        currentDoors = FindObjectsOfType<Door>();
        foreach(Door door in currentDoors)
        {
            if(!door.connected)
            {
                Instantiate(wall, door.transform.position, door.transform.rotation);
                Destroy(door);
            }
        }
    }
}
