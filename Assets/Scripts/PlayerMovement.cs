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

        //Vector3 up = transform.TransformDirection(Vector3.up) * 4 / 10;
        ////Debug.DrawRay(transform.position, up, Color.green);
        //Vector3 down = transform.TransformDirection(Vector3.up) * -4 / 10;
        ////Debug.DrawRay(transform.position, down, Color.green);
        //Vector3 right = transform.TransformDirection(Vector3.right) * 4 / 10;
        ////Debug.DrawRay(transform.position, right, Color.green);
        //Vector3 left = transform.TransformDirection(Vector3.right) * -4 / 10;
        ////Debug.DrawRay(transform.position, left, Color.green);

    }

    //void OnDrawGizmos()
    //{
    //    if (isHit)
    //    {
    //        Debug.Log("Hit shit");
    //        Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
    //        Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.localScale);
    //    }
    //    else
    //    {
    //        //Draw a Ray forward from GameObject toward the maximum distance
    //        Gizmos.DrawRay(transform.position, transform.up * 100);
    //        Debug.Log("No hit shit");
    //        //Draw a cube at the maximum distance
    //        Gizmos.DrawWireCube(transform.position + transform.up * 100, transform.localScale);
    //    }
    //}

    //void OnCollisionEnter(Collision col)
    //{
    //    Debug.Log("Can Move");

    //    if (col != null)
    //    {
    //        Debug.Log("Can Move");
    //        canMove = false;
    //    }
    //}
}
