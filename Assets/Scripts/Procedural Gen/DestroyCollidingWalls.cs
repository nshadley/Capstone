using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollidingWalls : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("theres a collision");
        if(col.gameObject.tag == "Wall" || col.gameObject.tag == "SideWall")
        {
            //Debug.Log("the collision is with a wall");
            if (col.gameObject.layer == 8)
            {
                Destroy(this);
            }
            else if(gameObject.layer != 8)
            {
                Destroy(col.gameObject);
            }
        }
    }
}
