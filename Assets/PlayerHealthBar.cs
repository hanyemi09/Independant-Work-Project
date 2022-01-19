using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] Slider m_HealthFillImage;
    [SerializeField] Health m_PlayerHealth;
    [SerializeField] Gradient m_ColorGradient;
    [SerializeField] Image m_Fill;
    // Start is called before the first frame update
    void Start()
    {
        m_HealthFillImage = GetComponent<Slider>();
        m_Fill = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        m_Fill.color = m_ColorGradient.Evaluate(1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_PlayerHealth)
            return;

        m_HealthFillImage.value = m_PlayerHealth.GetHealth() / m_PlayerHealth.GetMaxHealth();
        m_Fill.color = m_ColorGradient.Evaluate(m_HealthFillImage.normalizedValue);
    }

    public void Initialize(GameObject player)
    {
        if(m_PlayerHealth == null)
            m_PlayerHealth = player.GetComponent<Health>();
    }


}
