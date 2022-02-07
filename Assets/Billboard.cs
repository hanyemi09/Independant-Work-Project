using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    Transform m_Camera;

    // Start is called before the first frame update
    void Start()
    {
        m_Camera = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Change Rotation to allow billboarding
        //m_Camera.LookAt(transform.position + m_Camera.forward);
    }
}
