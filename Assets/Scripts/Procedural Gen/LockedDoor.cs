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
        if (Vector3.Distance(gm.playerRef.transform.position, transform.position) < interactionDistance)
        {
            gm.ingameUIRef.ShowDoorText();
        }
        else
        {
            gm.ingameUIRef.HideDoorText();
        }
    }

    void OnMouseDown()
    {
        if (Vector3.Distance(gm.playerRef.transform.position, transform.position) < interactionDistance)
        {
            if(!gm.playerScriptRef.haveKey)
            {
                //display door is locked
                gm.ingameUIRef.ShowDoorText();
            }
            else
            {
                gm.ingameUIRef.HideDoorText();
                FindObjectOfType<AudioScript>().soundEffectSource.PlayOneShot(FindObjectOfType<AudioScript>().doorSound);
                //unlock door
                Destroy(gameObject);
            }
        }
        
    }
}
