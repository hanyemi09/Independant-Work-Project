using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GameEnding : MonoBehaviour
{
    [SerializeField] float m_Timer;
    [SerializeField] Text m_Text;
    [SerializeField] string m_EndingString;
    bool bolean = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer -= Time.deltaTime;
        m_Text.text = m_EndingString + m_Timer.ToString();
        
        if(m_Timer < 0 && !bolean)
        {
            bolean = true;
            if(bolean)
            {
                PhotonNetwork.LoadLevel("DemoAsteroids-LobbyScene");
            }
        }
    }
}
