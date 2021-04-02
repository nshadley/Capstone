using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Door
{
    GameManager gm;
    [SerializeField]
    float interactionDistance = 2.5f;

    public override void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {
        if (Vector3.Distance(gm.playerRef.transform.position, transform.position) < interactionDistance)
        {
            if(!gm.playerScriptRef.haveKey)
            {
                //display door is locked
            }
            else
            {
                //unlock door
            }
        }
    }
}
