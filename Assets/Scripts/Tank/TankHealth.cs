using UnityEngine;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public float m_StartingHealth = 100f;
    public Slider m_Slider;
    public Image m_FillImage;
    public Color m_FullHealthColor = Color.green;
    public Color m_ZeroHealthColor = Color.red;
    public GameObject m_ExplosionPrefab;

    [HideInInspector]public float m_CurrentHealth;

    private AudioSource m_ExplosionAudio;
    private ParticleSystem m_ExplosionParticles;
    private Transform m_ExplosionTransform;
    private ModifiersManager m_TankModifiers;
    private bool m_Dead;
    private Transform m_transform;


    private void Awake()
    {
        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>();
        m_ExplosionAudio = m_ExplosionParticles.gameObject.GetComponent<AudioSource>();
        m_ExplosionTransform = m_ExplosionParticles.gameObject.GetComponent<Transform>();
        m_transform = gameObject.GetComponent<Transform>();
        m_TankModifiers = gameObject.GetComponent<ModifiersManager>();
        m_ExplosionParticles.gameObject.SetActive(false);
    }


    private void OnEnable()
    {
        m_CurrentHealth = m_StartingHealth;
        m_Dead = false;

        SetHealthUI();
    }


    public void TakeDamage(float amount)
    {
        if (!m_TankModifiers.States.HasFlag(ModifierStates.MODIFIER_STATE_ATTACK_IMMUNE))
        {
            // Adjust the tank's current health, update the UI based on the new health and check whether or not the tank is dead.
            m_CurrentHealth -= amount;
            SetHealthUI();
            if (m_CurrentHealth <= 0f && !m_Dead)
                OnDeath();
        }
    }


    private void SetHealthUI()
    {
        // Adjust the value and colour of the slider.
        m_Slider.value = (m_CurrentHealth / m_StartingHealth) * 100f;
        m_FillImage.color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, m_CurrentHealth / m_StartingHealth);
    }


    private void OnDeath()
    {
        // Play the effects for the death of the tank and deactivate it.
        m_Dead = true;
        m_ExplosionTransform.position = m_transform.position;
        m_ExplosionParticles.gameObject.SetActive(true);
        m_ExplosionParticles.Play();
        m_ExplosionAudio.Play();
        gameObject.SetActive(false);
    }
}