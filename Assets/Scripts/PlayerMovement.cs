using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerMovement : MonoBehaviour
{

    public GameObject bulletPrefab;

    FixedJoystick movementJoystick;
    FixedJoystick attackJoystick;
    BoxCollider boxCollider;
    RaycastHit hit;
    GameObject lastHit;

    Weapon weapon;
    Transform shootPoint;
    Vector3 moveDelta;
    Vector3 collision = Vector3.zero;
    Vector3 movePos, lastPos;
    float rotationSpeed = 10f;
    float borderDirControl = 0.15f;
    float moveSpeed = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        movementJoystick = GameObject.Find("MovementJoystick").GetComponent<FixedJoystick>();
        attackJoystick = GameObject.Find("AttackJoystick").GetComponent<FixedJoystick>();
        shootPoint = GameObject.Find("ShootPoint").GetComponent<Transform>();
        weapon = GameObject.Find("WeaponHolder").transform.GetChild(0).GetComponent<Weapon>();
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
            // If player is attacking, get use the direction of attack joystick instead
            Vector3 dirAttack = new Vector3(ax, 0, az);
      
            if(dirAttack.magnitude > 0)
            {
                if(CheckForPlayerAttackValid())
                {
                    PlayerAttack();
                }
            }
            if (dirAttack.magnitude > borderDirControl)
            {
                dir = dirAttack;
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

    public bool CheckForPlayerAttackValid()
    {
        return true;
        // Return is player attack valid
    }

    public void PlayerAttack()
    {
        weapon.WeaponAttack();
    }
}
