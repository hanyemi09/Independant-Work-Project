using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 1f;
    public Rigidbody rb;

    FixedJoystick movementJoystick;
    FixedJoystick attackJoystick;
    BoxCollider boxCollider;
    RaycastHit hit;
    GameObject lastHit;

    Vector3 moveDelta;
    Vector3 collision = Vector3.zero;
    Vector3 movePos, lastPos;
    bool isHit;
    bool canMove;
    bool isMoving;
    [SerializeField]float rotationSpeed = 10f;
    float borderDirControl = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        movementJoystick = GameObject.Find("MovementJoystick").GetComponent<FixedJoystick>();
        attackJoystick = GameObject.Find("AttackJoystick").GetComponent<FixedJoystick>();
    }

    // Update is called once per frame
    void Update()
    {

        float x = movementJoystick.Horizontal;
        float z = movementJoystick.Vertical;

        Vector3 dir = new Vector3(x, 0, z);

        if (attackJoystick != null)
        {
            float ax = attackJoystick.Horizontal;
            float az = attackJoystick.Vertical;

            // Get direction of the player is moving


            //If player is attacking, get use the direction of attack joystick instead
            Vector3 dirAttack = new Vector3(ax, 0, az);
            Debug.Log(dirAttack.magnitude);
            if (dirAttack.magnitude > borderDirControl)
            {
                dir = dirAttack;
                Debug.Log("Enter");
            }
        }

        // Movement will always be according to the player joystick
        moveDelta = new Vector3(x, 0, z);

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

        // Swap sprite directions
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
  
    }

    public void playerAttack()
    {

    }
}
