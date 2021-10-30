using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 1f;
    public Rigidbody rb;

    FixedJoystick joystick;
    //AttackJoystick attackJoystick;
    BoxCollider boxCollider;
    RaycastHit hit;
    GameObject lastHit;

    bool isHit;
    Vector3 moveDelta;
    Vector3 collision = Vector3.zero;
    Vector3 movePos, lastPos;
    bool canMove;
    bool isMoving;
    [SerializeField]float rotationSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        joystick = FindObjectOfType<FixedJoystick>();
        //attackJoystick = FindObjectOfType<AttackJoystick>();
    }

    // Update is called once per frame
    void Update()
    {

        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        if (joystick.Horizontal != 0 && joystick.Vertical != 0)
        {
            JoystickMovement();
        }


        //float ax = attackJoystick.Horizontal;
        //float ay = attackJoystick.Vertical;

        // Get direction of the player is moving
        Vector3 dir = new Vector3(x, 0, z);

        // If player is attacking, get use the direction of attack joystick instead
        //Vector3 dirAttack = new Vector3(ax,ay,0);
        // if(dirAttack != Vector3.zero)
        //{
        //    dir = dirAttack;
        //}

        // Movement will always be according to the player joystick
        moveDelta = new Vector3(x, 0, z);

        // transform.up direction = dir

        // Convert direction to the angle between -180 to 180
        // Vector3 lookDir = new Vector3(10, 0, 30) - transform.position;
        // Direction
        if (dir != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            //Debug.Log("Rotation: " + rotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }

        lastPos = movePos;
        movePos = transform.position;
        isMoving = true;

        if (lastPos == movePos)
        {
            isMoving = false;
        }

        // Moving
        transform.Translate(moveDelta * Time.deltaTime * moveSpeed, Space.World);

        // Attack

        // Swap sprite directions
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        void JoystickMovement()
        {

        }

        public void Attack()
        {

        }
    }
}
