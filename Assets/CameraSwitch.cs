using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{

    [SerializeField] GameObject m_Left;
    [SerializeField] GameObject m_Right;
    GameObject m_Canvas;
    CameraMotor m_CameraMotor;
    // Start is called before the first frame update
    void Start()
    {
        m_Canvas = GameObject.Find("Canvas");
        m_CameraMotor = GetComponent<CameraMotor>();
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

}
