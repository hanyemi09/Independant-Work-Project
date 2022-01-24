using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CameraMotor : MonoBehaviour
{
    PlayerList m_PlayerList;
    public Transform lookAt;
    public float boundX = 0f;
    public float boundZ = 0f;
    float timeToLastTouch;
    int m_PlayerIndex;
    Vector3 mousePos;

    void Start()
    {
        lookAt = GameObject.FindWithTag("Player").transform;
        m_PlayerList = GameObject.Find("EventSystem").GetComponent<PlayerList>();
        m_PlayerIndex = m_PlayerList.GetPlayerIndexByViewID(lookAt.gameObject.GetComponent<PhotonView>().ViewID);
    }

    // Change everything to top down view;
    void LateUpdate()
    {
        Vector3 savedLastPos = Vector3.zero;
        if (lookAt != null)
        {

            Vector3 delta = Vector3.zero;

            Vector3 lastPos = Vector3.zero;
            savedLastPos = lastPos;
            //Vector3 mousePos = Vector3.zero;
            float deltaX = lookAt.position.x - transform.position.x;
            if (deltaX > boundX || deltaX < -boundX)
            {
                if (transform.position.x < lookAt.position.x)
                {
                    delta.x = deltaX - boundX;
                }
                else
                {
                    delta.x = deltaX + boundX;
                }
            }
            float deltaZ = lookAt.position.z - transform.position.z;
            if (deltaZ > boundZ || deltaZ < -boundZ)
            {
                if (transform.position.z < lookAt.position.z)
                {
                    delta.z = deltaZ - boundZ;
                }
                else
                {
                    delta.z = deltaZ + boundZ;
                }
            }

            lastPos = Vector3.zero;

            // Zoom in

            // Zoom out

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                // Update the Text on the screen depending on current position of the touch each frame
                Debug.Log(touch.position);
            }
            transform.position += new Vector3(delta.x, 0, delta.z);
        }
        else if(lookAt == null)
        {
            transform.position = savedLastPos;
            SwitchCamera(false);
        }
    }

    public void SwitchCamera(bool Reverse)
    {
        CameraSwitch m_CameraSwitch = GetComponent<CameraSwitch>();
        m_CameraSwitch.enabled = true;
        //m_CameraSwitch.PlayerSpectatorMode();
        if (Reverse)
        {
            m_PlayerIndex -= 1;
        }
        else
        {
            m_PlayerIndex += 1;
        }

        //if (m_PlayerIndex >= m_PlayerList.GetListLength())
        //{
        //    m_PlayerIndex = 0;
        //}
        //else
        //{
        //    m_PlayerIndex = m_PlayerList.GetListLength() - 1;
        //}
        lookAt = m_PlayerList.GetPlayerByIndex(m_PlayerIndex).transform;


    }

}
