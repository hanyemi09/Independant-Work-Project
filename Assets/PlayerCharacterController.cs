using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacterController : MonoBehaviour
{
    CharacterController m_Controller;
    FixedJoystick m_MovementJoyStick;
    FixedJoystick m_AttackJoystick;
    PhotonView m_PhotonView;
    PlayerWeaponsController m_PlayerWeaponsController;
    PlayerList m_PlayerList;
    float RotationSpeed = 10f;
    float BorderDirControl = 0.15f;
    float MoveSpeed = 5f;
    float MoveSpeedAmt = 5f;
    float MoveSpeedMultiplier = 1f;

    Vector3 MoveDelta;

    public Vector3 CharacterVelocity { get; set; }
    public bool IsGrounded { get; private set; }

    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_PhotonView = GetComponent<PhotonView>();
        m_MovementJoyStick = GameObject.Find("MovementJoystick").GetComponent<FixedJoystick>();
        m_AttackJoystick = GameObject.Find("AttackJoystick").GetComponent<FixedJoystick>();
        m_PlayerWeaponsController = GetComponent<PlayerWeaponsController>();
        m_PlayerList = GameObject.Find("EventSystem").GetComponent<PlayerList>();

        if (m_PlayerList)
            m_PlayerList.AddToList(this.gameObject);
    }

    void Update()
    {
        if (!m_PhotonView.IsMine)
            return;

        float x = m_MovementJoyStick.Horizontal;
        float z = m_MovementJoyStick.Vertical;

        Vector3 dir = new Vector3(x, 0, z);

        if (m_AttackJoystick != null)
        {
            float ax = m_AttackJoystick.Horizontal;
            float az = m_AttackJoystick.Vertical;

            // Get direction of the player is moving
            // If player is attacking, get use the direction of attack joystick instead
            Vector3 dirAttack = new Vector3(ax, 0, az);

            if (dirAttack.magnitude > BorderDirControl)
            {
                dir = dirAttack;
            }

            if (dirAttack.magnitude > 0)
            {
                // Attacking
                m_PlayerWeaponsController.HandleShoot(dir);
            }
        }

        // Movement will always be according to the player joystick
        MoveDelta = new Vector3(x, 0, z);

        // Direction
        if (dir != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
            //Debug.Log("Rotation: " + rotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * RotationSpeed);
        }

        MoveSpeedAmt = MoveSpeed * MoveSpeedMultiplier;

        // Moving
        CharacterVelocity = MoveDelta * Time.deltaTime * MoveSpeedAmt;
        m_Controller.Move(CharacterVelocity);
    }
}
