using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Image HealthFillImage;
    Health m_PlayerHealth;

    // Start is called before the first frame update
    void Start()
    {
        HealthFillImage = GameObject.Find("HealthFillUI").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        HealthFillImage.fillAmount = m_PlayerHealth.GetHealth() / m_PlayerHealth.GetMaxHealth();
    }

    public void Initialize(GameObject player)
    {
        m_PlayerHealth = player.GetComponent<Health>();
    }


}
