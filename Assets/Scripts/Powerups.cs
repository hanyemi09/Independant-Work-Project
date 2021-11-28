using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Powerups : MonoBehaviour
{

    enum POWERUPTYPE
    {
        HEALTH,
        DAMAGEBUFF,
        ATKSPEEDBUFF,
        MOVESPEEDBUFF,
        TOTAL,
    }

    [SerializeField]POWERUPTYPE powerupType;
    [SerializeField] float healAmount;
    [SerializeField] float dmgMultiplier;
    [SerializeField] float atkSpdMultiplier;
    [SerializeField] float moveSpdMultiplier;
    [SerializeField] float duration;

    PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        PhotonView pv = col.gameObject.GetComponent<PhotonView>();
        
        switch (powerupType)
        {
            case POWERUPTYPE.HEALTH:
                if (pv != null)
                    pv.RPC("HealPlayer", RpcTarget.All, healAmount);
                break;
            case POWERUPTYPE.DAMAGEBUFF:
                Weapon weap = pv.transform.Find("WeaponHolder").GetChild(0).gameObject.GetComponent<Weapon>();
                if (weap != null)
                    weap.DamageBuff(dmgMultiplier, duration);
                break;
            case POWERUPTYPE.ATKSPEEDBUFF:
                Weapon wea = pv.transform.Find("WeaponHolder").GetChild(0).gameObject.GetComponent<Weapon>();
                if (wea != null)
                    wea.AtkSpdBuff(atkSpdMultiplier, duration);
                break;
            case POWERUPTYPE.MOVESPEEDBUFF:
                PlayerMovement pm = pv.gameObject.GetComponent<PlayerMovement>();
                if (pm != null)
                    pm.MoveSpeedBuff(moveSpdMultiplier, duration);
                break;

        }
        GetComponent<PhotonView>().RPC("DestroyPowerup", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void DestroyPowerup()
    {
        if (this.photonView.IsMine)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

}
