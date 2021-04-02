using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCollidingWalls : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Wall")
        {
            Destroy(this);
        }
    }
}
