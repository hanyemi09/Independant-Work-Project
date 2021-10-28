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

    GameObject lastHit;
    Vector3 collision = Vector3.zero;

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

        //Debug.Log(x);
        //Debug.Log(y);
        //float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");

        //horizontalMove = joystick.Horizontal * runSpeed;

        // Reset moveDelta
        moveDelta = new Vector3(x, y, 0);

        // Swap sprite directions
        //if(moveDelta.x > 0)
        //{
        //    transform.localScale = Vector3.one;
        //}
        //else if(moveDelta.x < 0)
        //{
        //    transform.localScale = new Vector3(-1, 1, 1);
        //}

        // Insert collision checker code
        // Check if there is anything along the sides of the player
        // If nothing is infront of the collider, player can move forward
        // else player cannot move


        // Move
        transform.Translate(moveDelta * Time.deltaTime * moveSpeed);

        Vector3 up = transform.TransformDirection(Vector3.up) * 4 / 10;
        Debug.DrawRay(transform.position, up, Color.green);
        var ray = new Ray(this.transform.localPosition, up);
        RaycastHit hit;

        // Shoot ray
        // Check if hit is on a particular layer
        // If ray is touching something, player cannot move that way
        // If ray is not touching anything, player can move

        Vector3 down = transform.TransformDirection(Vector3.up) * -4 / 10;
        Debug.DrawRay(transform.position, down, Color.green);

        Vector3 right = transform.TransformDirection(Vector3.right) * 4 / 10;
        Debug.DrawRay(transform.position, right, Color.green);

        Vector3 left = transform.TransformDirection(Vector3.right) * -4 / 10;
        Debug.DrawRay(transform.position, left, Color.green);
        //hit = Physics.BoxCast(transform.position, boxCollider.size, 0, new Vector3(0, moveDelta.y,0), Mathf.Abs(moveDelta * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        //Debug.Log(hit);
        //if (hit.collider == null)
        //{

        //}

        //hit = Physics.BoxCast(transform.position, boxCollider.size, 0, new Vector3(moveDelta.x, 0, 0), Mathf.Abs(moveDelta * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        //Debug.Log(hit);
        //if (hit.collider == null)
        //{
        //    transform.Translate(moveDelta * Time.deltaTime * moveSpeed);
        //}
    }
}
