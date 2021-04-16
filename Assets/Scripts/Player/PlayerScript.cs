using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private CharacterController controller;
    protected float xMove;
    protected float zMove;
    protected Vector3 move;

    [SerializeField]
    protected float movespeed = 10f;

    //inventory variables
    public bool haveKey;
    public bool win;

    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    public static PlayerScript instance;

    // Update is called once per frame
    void Update()
    {
        zMove = Input.GetAxis("Vertical") * movespeed;
        xMove = Input.GetAxis("Horizontal") * movespeed;

        move = transform.right * xMove + transform.forward * zMove;

        controller.Move(move * movespeed * Time.deltaTime);
    }
}
