using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Generator : MonoBehaviour
{
    [SerializeField]
    int numberOfRooms = 2;

    [SerializeField]
    List<GameObject> availableRooms = new List<GameObject>();

    [SerializeField]
    List<GameObject> puzzleRooms = new List<GameObject>();

    List<GameObject> pickedRooms = new List<GameObject>();

    List<Door> currentDoors = new List<Door>();

    Door selectedDoor;

    GameManager gm;

    [SerializeField]
    GameObject wall;

    void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

    ////Start is called before the first frame update
    void Start()
    {
        GenerateRooms();
        PlaceRooms();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    void GenerateRooms()
    {
        AddPuzzleRooms();
        Debug.Assert(pickedRooms.Count > 0, "No puzzle rooms were picked");
        AddRandomRooms();

        //shuffle rooms
        pickedRooms = ShuffleList<GameObject>(pickedRooms);

        //if a dead end is the first room, shuffle again
        if (pickedRooms[0].tag == "Dead End")
        {
            pickedRooms = ShuffleList<GameObject>(pickedRooms);
        }
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
        currentDoors.AddRange(FindObjectsOfType<Door>());

        Door doorCheck = currentDoors[Random.Range(0, currentDoors.Count)];


        while (doorCheck.connected)
        {
            doorCheck = currentDoors[Random.Range(0, currentDoors.Count)];
        }

        selectedDoor = doorCheck;
    }

    void PlaceRooms()
    {
        foreach (GameObject room in pickedRooms)
        {
            

            DeleteDoorsWithWalls();

            PickADoor();
            Debug.Log("Current door count " + currentDoors.Count);
            Room roomScript = room.GetComponent<Room>();

            GameObject newRoom = Instantiate(room, selectedDoor.transform.position, room.transform.rotation);

            Door chosenDoor = newRoom.GetComponent<Room>().ChooseDoor();
            GameObject childDoor = chosenDoor.gameObject;
            GameObject toDoor = selectedDoor.gameObject;

            Debug.Log(newRoom.name + " is connecting to " + toDoor.transform.parent.name);

            //first rotate rooms to match horizontally or vertically
            if (childDoor.GetComponent<Door>().IsDoorHorizontal() != toDoor.GetComponent<Door>().IsDoorHorizontal())
            {
                roomScript.RotateRoom(newRoom);
            }

            if (toDoor.tag == "StartRoom")
            {
                //this only works becauase the start room is at the origin
                newRoom.transform.position += toDoor.transform.position;
                Debug.Log("The to door is a starting door");
                if (childDoor.transform.position != toDoor.transform.position)
                {
                    roomScript.TurnRoomAround(newRoom);
                }
            }
            else
            {
                //change room loaction based on position of doors
                if (childDoor.transform.position.x != toDoor.transform.position.x)
                {
                    Vector3 xDifference = new Vector3((toDoor.transform.position.x - childDoor.transform.position.x), 0, 0);
                    Debug.Log("xDifference is " + xDifference);
                    newRoom.transform.position += xDifference;
                }
                if (childDoor.transform.position.z != toDoor.transform.position.z)
                {
                    Vector3 zDifference = new Vector3(0, 0, (toDoor.transform.position.z - childDoor.transform.position.z));
                    Debug.Log("zDifference is " + zDifference);
                    newRoom.transform.position += zDifference;
                }

                //rooms need to be turned around and moved again if positions are still not correct
                if (childDoor.transform.position != toDoor.transform.position ||
                    (childDoor.transform.parent.transform.position == toDoor.transform.parent.transform.position))
                {
                    roomScript.TurnRoomAround(newRoom);

                    //change room loaction based on position of doors
                    if (childDoor.transform.position.x != toDoor.transform.position.x)
                    {
                        Vector3 xDifference = new Vector3((toDoor.transform.position.x - childDoor.transform.position.x), 0, 0);
                        Debug.Log("xDifference is " + xDifference);
                        newRoom.transform.position += xDifference;
                    }
                    if (childDoor.transform.position.z != toDoor.transform.position.z)
                    {
                        Vector3 zDifference = new Vector3(0, 0, (toDoor.transform.position.z - childDoor.transform.position.z));
                        Debug.Log("zDifference is " + zDifference);
                        newRoom.transform.position += zDifference;
                    }
                }

                Room[] currentRooms = FindObjectsOfType<Room>();
                foreach(Room r in currentRooms)
                {
                    if(r.gameObject != newRoom && r.gameObject.transform.position == newRoom.transform.position)
                    {
                        //rooms collide, not sure what to do about this
                    }
                }

            }

            //mark doors as connected
            childDoor.GetComponent<Door>().connected = true;
            toDoor.GetComponent<Door>().connected = true;
            selectedDoor.connected = true;
            chosenDoor.connected = true;

            //delete doors if they are not locked
            if (!childDoor.GetComponent<LockedDoor>() && !toDoor.GetComponent<LockedDoor>())
            {
                Destroy(childDoor);
                Destroy(toDoor);
            }
            else if (childDoor.GetComponent<LockedDoor>())
            {
                Destroy(toDoor);
            }
            else
            {
                Destroy(childDoor);
            }

            //find all unconnected doors and mark them as connected if collides with a wall or another door
            List<Door> openDoors = new List<Door>(FindObjectsOfType<Door>());
            for (int index = 0; index < openDoors.Count; index++)
            {
                if (!openDoors[index].connected)
                {
                    bool theresAWall = false;
                    Collider[] overlaps = Physics.OverlapBox(openDoors[index].transform.position, new Vector3(.5f, .5f, .5f));
                    foreach (Collider col in overlaps)
                    {
                        if (col.gameObject.tag == "Wall" || col.gameObject.tag == "Door")
                        {
                            theresAWall = true;
                        }
                    }
                    if (theresAWall)
                    {
                        openDoors[index].connected = true;
                    }
                }
                index++;
            }
        }
    }

    void AddPuzzleRooms()
    {
        foreach (GameObject r in puzzleRooms)
        {
            Room tempRoom = r.GetComponent<Room>();
            pickedRooms.Add(r);
        }
    }

    void AddRandomRooms()
    {
        int deadEndCount = 0;
        while (pickedRooms.Count < numberOfRooms)
        {
            //bool itsANewRoom = false;
            GameObject aRoom = availableRooms[Random.Range(0, availableRooms.Count)];
            if (aRoom.tag == "Dead End")
            {
                if (deadEndCount < numberOfRooms / 2)
                {
                    pickedRooms.Add(aRoom);
                    deadEndCount++;
                }
            }
            else
            {
                pickedRooms.Add(aRoom);
            }

        }
    }

    void DeleteDoorsWithWalls()
    {
        //if a door is colliding with a wall, delete it

        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        currentDoors.AddRange(FindObjectsOfType<Door>());

        for(int index = 0; index < currentDoors.Count; index++)
        {
            foreach (GameObject wall in walls)
            {
                if (currentDoors[index].gameObject.transform.position == wall.transform.position)
                {
                    Destroy(currentDoors[index].gameObject);
                }
            }
        }
    }
}
