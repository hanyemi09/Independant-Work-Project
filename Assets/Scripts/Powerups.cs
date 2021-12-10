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
    float immuneTime = 1f;
    PhotonView photonView;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Spawned");
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        immuneTime -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider col)
    {
        PhotonView pv = col.gameObject.GetComponent<PhotonView>();
        if (col.gameObject.layer != 3)
            return;

        if(immuneTime < 0)
        {
            switch (powerupType)
            {
                case POWERUPTYPE.HEALTH:
                    if (pv != null)
                        pv.RPC("HealPlayer", RpcTarget.All, healAmount);
                    Debug.Log("Healing");
                    break;
                case POWERUPTYPE.DAMAGEBUFF:
                    Weapon weap = pv.transform.Find("WeaponHolder").GetChild(0).gameObject.GetComponent<Weapon>();
                    if (weap != null)
                        weap.DamageBuff(dmgMultiplier, duration);
                    Debug.Log("Damage Buff");
                    break;
                case POWERUPTYPE.ATKSPEEDBUFF:
                    Weapon wea = pv.transform.Find("WeaponHolder").GetChild(0).gameObject.GetComponent<Weapon>();
                    if (wea != null)
                        wea.AtkSpdBuff(atkSpdMultiplier, duration);
                    Debug.Log("Attack Speed Buff");
                    break;
                case POWERUPTYPE.MOVESPEEDBUFF:
                    PlayerMovement pm = pv.gameObject.GetComponent<PlayerMovement>();
                    if (pm != null)
                        pm.MoveSpeedBuff(moveSpdMultiplier, duration);
                    Debug.Log("Move Speed");
                    break;

            }
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
