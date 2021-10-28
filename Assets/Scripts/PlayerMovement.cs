using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    FixedJoystick joystick;
    BoxCollider2D boxCollider;
    Vector3 moveDelta;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        joystick = FindObjectOfType<FixedJoystick>();
    }

    // Update is called once per frame
    void Update()
    {

        float x = joystick.Horizontal;
        float y = joystick.Vertical;

        Debug.Log(x);
        Debug.Log(y);
        //float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");

        //horizontalMove = joystick.Horizontal * runSpeed;

        // Reset moveDelta
        moveDelta = new Vector3(x, y, 0);
        
        // Swap sprite directions
        if(moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if(moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Move
        transform.Translate(moveDelta * Time.deltaTime);
    }
}
