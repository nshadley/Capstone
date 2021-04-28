using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public GameManager gm;

    Light itemLight;

    public GameObject pickUpParticle;

    [SerializeField]
    float interactionDistance = 2.5f;


    // Start is called before the first frame update
    public virtual void Start()
    {
        gm = FindObjectOfType<GameManager>();
        itemLight = GetComponentInChildren<Light>();

        itemLight.enabled = false;
    }

    // Update is called once per frame
    public virtual void Update()
    {


        if (Vector3.Distance(gm.playerRef.transform.position, transform.position) < interactionDistance)
        {
            itemLight.enabled = true;
            if(Input.GetMouseButton(0))
            {
                PickUp();
            }
        }
        else
        {
            itemLight.enabled = false;
        }
    }

    public virtual void PickUp()
    {
        //specific item stuff to be overridden here
        
        Destroy(gameObject);
    }
}
