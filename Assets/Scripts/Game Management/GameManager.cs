using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject playerRef;
    public PlayerScript playerScriptRef;
    public PuzzleManager puzzleManagerRef;

    public InGameUI ingameUIRef;

    public static GameManager Instance { get; set; }

    void Awake()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
        playerScriptRef = playerRef.GetComponent<PlayerScript>();
        ingameUIRef = GameObject.FindObjectOfType<InGameUI>();
        puzzleManagerRef = GameObject.FindObjectOfType<PuzzleManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
