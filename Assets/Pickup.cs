using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
[RequireComponent(typeof(PhotonView)), RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Collider))]

public class Pickup : MonoBehaviour
{

    public GameObject m_PickupVFX;
    public AudioClip m_PickupSFX;

    [Tooltip("Frequency at which the item will move up and down")]
    [SerializeField] protected float m_VerticalBobFrequency = 1;
    [Tooltip("Distance the item will move up and down")]
    [SerializeField] protected float m_BobbingAmount = 1f;
    [Tooltip("Rotation angle per second")]
    [SerializeField] protected float m_RotatingSpeed = 45f;
    Rigidbody m_PickupRigidbody;
    Collider m_Collider;
    protected Vector3 m_StartPosition;
    protected PhotonView m_PhotonView;
    // Start is called before the first frame update
    public void Start()
    {
        m_PhotonView = this.GetComponent<PhotonView>();

        m_PickupRigidbody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();

        // ensure the physics setup is a kinematic rigidbody trigger
        m_PickupRigidbody.isKinematic = true;
        m_Collider.isTrigger = true;

        m_StartPosition = transform.position;

        m_RotatingSpeed = Random.Range(45f, 90f);
        m_VerticalBobFrequency = Random.Range(-0.5f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_PhotonView.IsMine)
            return;

        // Handle bobbing
        float bobbingAnimationPhase = ((Mathf.Sin(Time.time * m_VerticalBobFrequency) * 0.5f) + 0.5f) * m_BobbingAmount;
        transform.position = m_StartPosition + Vector3.up * bobbingAnimationPhase;

        // Handle rotating
        transform.Rotate(Vector3.up, m_RotatingSpeed * Time.deltaTime, Space.Self);
    }

    void OnTriggerEnter(Collider other)
    {
  
    }

    public void PlayPickupFeedback()
    {
        if (m_PickupSFX)
        {

        }

        if (m_PickupVFX)
        {

        }
    }

    [PunRPC]
    public void DestroyPickup()
    {
        PlayPickupFeedback();
        PhotonNetwork.Destroy(this.gameObject);
    }
}
