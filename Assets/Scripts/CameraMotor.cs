    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{

    public Transform lookAt;
    public float boundX = 2f;
    public float boundZ = 2f;
    float timeToLastTouch;

    Vector3 mousePos;

    void Start()
    {
        boundX = boundZ = 2f;
    }
    // Change everything to top down view;
    void LateUpdate()
    {
        Vector3 delta = Vector3.zero;
        
        Vector3 lastPos = Vector3.zero; 
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
        if (deltaZ > boundZ|| deltaZ < -boundZ)
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
}
