using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[ExecuteInEditMode]
public class PlayerMovement : MonoBehaviour
{
    PhotonView photonView;

    FixedJoystick movementJoystick;
    FixedJoystick attackJoystick;
    BoxCollider boxCollider;
    RaycastHit hit;
    GameObject lastHit;
    PlayerWeaponsManager playerWeaponsManager;
    Vector3 moveDelta;
    Vector3 collision = Vector3.zero;
    Vector3 movePos, lastPos;
    float rotationSpeed = 10f;
    float borderDirControl = 0.15f;
    float moveSpeed = 5f;
    float moveSpeedAmt = 5f;
    float moveSpeedMultiplier = 1f;
    Vector3 transformSize;
    public PlayerList pl;
    // Start is called before the first frame update

    void Awake()
    {

    }
    void Start()
    {
        moveSpeedAmt = moveSpeed;
        Physics.IgnoreLayerCollision(3, 3);
        boxCollider = GetComponent<BoxCollider>();
        movementJoystick = GameObject.Find("MovementJoystick").GetComponent<FixedJoystick>();
        attackJoystick = GameObject.Find("AttackJoystick").GetComponent<FixedJoystick>();
        playerWeaponsManager = GetComponent<PlayerWeaponsManager>();
        transformSize = transform.localScale;
        photonView = GetComponent<PhotonView>();
        pl = GameObject.FindObjectOfType<PlayerList>();
        pl.AddToList(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
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

                if (dirAttack.magnitude > borderDirControl)
                {
                    dir = dirAttack;
                }

                if (dirAttack.magnitude > 0)
                {
                    // Attacking
                    playerWeaponsManager.HandleShoot(dir);
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

            moveSpeedAmt = moveSpeed * moveSpeedMultiplier;

            // Moving
            transform.Translate(moveDelta * Time.deltaTime * moveSpeedAmt, Space.World);

            // Swap sprite directions
            if (moveDelta.x > 0)
            {
                transform.localScale = transformSize;
            }
            else if (moveDelta.x < 0)
            {
                transform.localScale = new Vector3(transformSize.x, transformSize.y, transformSize.z);
            }
        }
    }

    public void MoveSpeedBuff(float multiplier, float duration)
    {
        moveSpeedMultiplier = multiplier;
        StartCoroutine(MoveSpeedBuffDuration(duration));
    }

    IEnumerator MoveSpeedBuffDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        moveSpeedMultiplier = 1f;
    }
}
