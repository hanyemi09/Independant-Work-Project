using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 1f;
    FixedJoystick joystick;
    BoxCollider boxCollider;
    Vector3 moveDelta;

    RaycastHit hit;
    bool isHit;
    GameObject lastHit;
    Vector3 collision = Vector3.zero;
    Vector3 movePos, lastPos;
    bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        joystick = FindObjectOfType<FixedJoystick>();
    }

    // Update is called once per frame
    void Update()
    {

        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        // Reset moveDelta
        moveDelta = new Vector3(x, y, 0);
        //Vector3 dir = (lastPos - movePos).normalize;

        // Attack

        // get move direction

        // Swap sprite directions
        //if(moveDelta.x > 0)
        //{
        //    transform.localScale = Vector3.one;
        //}
        //else if(moveDelta.x < 0)
        //{
        //    transform.localScale = new Vector3(-1, 1, 1);
        //}

        // Moving
        transform.Translate(moveDelta * Time.deltaTime * moveSpeed);


    }

}
