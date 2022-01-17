using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPIckup : MonoBehaviour
{
    public Button WeaponPickupButton;
    // Start is called before the first frame update
    void Start()
    {
        WeaponPickupButton = GetComponent<Button>();
        
    }

    void TaskOnClick()
    {
    }
}
