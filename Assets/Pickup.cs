using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Pickup : MonoBehaviour
{

    public GameObject m_PickupVFX;
    public AudioClip m_PickupSFX;

    [Tooltip("Frequency at which the item will move up and down")]
    [SerializeField] float m_VerticalBobFrequency = 1f;
    [Tooltip("Distance the item will move up and down")]
    [SerializeField] float m_BobbingAmount = 1f;
    [Tooltip("Rotation angle per second")]
    [SerializeField] float m_RotatingSpeed = 360f;
    Vector3 m_StartPosition;
    Rigidbody m_PickupRigidbody;
    Collider m_Collider;
    // Start is called before the first frame update
    void Start()
    {
        m_PickupRigidbody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<Collider>();

        // ensure the physics setup is a kinematic rigidbody trigger
        m_PickupRigidbody.isKinematic = true;
        m_Collider.isTrigger = true;

        // Remember start position for animation
        m_StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
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
