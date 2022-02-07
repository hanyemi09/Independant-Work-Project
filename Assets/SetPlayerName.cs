using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class SetPlayerName : MonoBehaviour
{

    Text m_Text;
    // Start is called before the first frame update
    void Start()
    {
        m_Text = GetComponent<Text>();
        m_Text.text = this.gameObject.transform.parent.parent.gameObject.transform.gameObject.GetComponent<PhotonView>().Owner.NickName;
    }

}
