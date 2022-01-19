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
    Transform m_GroundCheck;
    PlayerHealthBar m_PlayerHealthBar;
    SpawnPlayers m_SpawnPlayers;
    float m_RotationSpeed = 10f;
    float m_BorderDirControl = 0.15f;
    float m_MoveSpeed = 5f;
    float m_MoveSpeedAmt = 5f;
    float m_MoveSpeedMultiplier = 1f;
    bool m_IsGrounded = false;
    [SerializeField] LayerMask m_GroundMask;
    float m_Gravity = -9.81f * 6; 
    Vector3 MoveDelta;
    float m_GroundDistance = 0.1f;
    Vector3 m_CharacterVelocity;
    public bool IsGrounded;

    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_PhotonView = GetComponent<PhotonView>();
        m_MovementJoyStick = GameObject.Find("MovementJoystick").GetComponent<FixedJoystick>();
        m_AttackJoystick = GameObject.Find("AttackJoystick").GetComponent<FixedJoystick>();
        m_PlayerWeaponsController = GetComponent<PlayerWeaponsController>();
        m_PlayerList = GameObject.FindObjectOfType<PlayerList>();
        m_PlayerHealthBar = GameObject.Find("HealthBar").GetComponent<PlayerHealthBar>();
        m_GroundCheck = gameObject.transform.GetChild(1).gameObject.GetComponent<Transform>();
        m_SpawnPlayers = GameObject.Find("EventSystem").GetComponent<SpawnPlayers>();

        if (m_PlayerList)
            m_PlayerList.AddToList(this.gameObject);

        if (m_PlayerHealthBar)
            m_PlayerHealthBar.Initialize(this.gameObject);
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

            if (dirAttack.magnitude > m_BorderDirControl)
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
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * m_RotationSpeed);
        }

        m_MoveSpeedAmt = m_MoveSpeed * m_MoveSpeedMultiplier;

        // Gravity
        MoveDelta.y += m_Gravity * Time.deltaTime;

        IsGrounded = Physics.CheckSphere(m_GroundCheck.position, m_GroundDistance, m_GroundMask);


        // Moving
        m_CharacterVelocity = MoveDelta * Time.deltaTime * m_MoveSpeedAmt;

        if (IsGrounded && MoveDelta.y < 0)
        {
            m_CharacterVelocity.y = -2f;
        }
        m_Controller.Move(m_CharacterVelocity);
    }
}
