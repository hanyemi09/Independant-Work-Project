    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{

    public Transform lookAt;
    public float boundX = 5f;
    public float boundY = 5f;
    float timeToLastTouch;

    Vector3 mousePos;

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
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        // Camera movement with touch
        //if (Input.GetMouseButton(0))
        //{
        //    lastPos = mousePos;
        //    mousePos = Input.mousePosition;

        //    //delta.x = lastPos.x - mousePos.x;
        //    //delta.y = lastPos.y - mousePos.y;
        //    if(lastPos != Vector3.zero)
        //    {
        //        delta.x = mousePos.x - lastPos.x;
        //        delta.y = mousePos.y - lastPos.y;

        //    }

        //    delta = delta * 0.1f;

        //}
        //else if(!Input.GetMouseButton(0))
        //{
        //    mousePos = Vector3.zero;
        //}

        lastPos = Vector3.zero;
        //Debug.Log("Delta: " + delta);
        //Debug.Log("Last pos: " + lastPos);
        //Debug.Log("Mouse pos: " + mousePos);

        // Zoom in

        // Zoom out

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // Update the Text on the screen depending on current position of the touch each frame
            Debug.Log(touch.position);
        }
        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}