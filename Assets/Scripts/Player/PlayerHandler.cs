using UnityEngine;
using UnityEngine.UI;

namespace TankBattle
{
    public class PlayerHandler : MonoBehaviour
    {
        [Header("PlayerComponents")]
        public Rigidbody m_Rigidbody;
        [SerializeField] private PlayerAttributes m_PlayerAttributes;
        [HideInInspector] public PlayerAttributes m_Attributes;
        [SerializeField] private PlayerInputController m_InputController;
        public PlayerAbilityHandler m_AbilityHandler;
        public PlayerWeaponHandler m_WeaponHandler;
        [HideInInspector]public PlayerMovement m_Movement;

        [Header("PlayerWeapon")]
        public Transform m_WeaponTransform;

        [Header("Player Health")]
        [SerializeField]
        private Canvas m_HealthCanvas;
        [SerializeField] private Slider m_HealthSlider;
        [SerializeField] private Image m_HealthSliderFillImage;
        [SerializeField] private Color m_FullHealthSliderColor = Color.green;
        [SerializeField] private Color m_ZeroHealthSliderColor = Color.red;

        [HideInInspector] public GameObject CurrentWeapon;

        private void Awake()
        {
            m_Attributes = Instantiate(m_PlayerAttributes);
            m_Movement = new PlayerMovement(this);
            m_AbilityHandler.m_PlayerHandler = this;
            m_WeaponHandler.m_PlayerHandler = this;
        }

        void Update()
        {
            SetHealthUI();
            RegenAttributes();
        }

        public void ResetPlayer()
        {
            m_Attributes.ResetAttributes();
            RemoveWeapon();
            RemoveAbility();
        }

        public void EnablePlayer()
        {
            m_InputController.enabled = true;
            m_HealthCanvas.enabled = true;
            m_Rigidbody.isKinematic = false;
        }

        public void DisablePlayer()
        {
            m_InputController.enabled = false;
            m_HealthCanvas.enabled = false;
            m_Rigidbody.isKinematic = true;
        }

        //public void SetWeapon(GameObject weaponPrefab)
        //{
        //    if (CurrentWeapon != null)
        //        Destroy(CurrentWeapon);

        //    CurrentWeapon = Instantiate(weaponPrefab, m_WeaponTransform);

        //    IWeapon weapon = CurrentWeapon.GetComponent<IWeapon>();
        //    weapon.m_PlayerHandler = this;

        //    MeshRenderer[] renderers = CurrentWeapon.GetComponents<MeshRenderer>();
        //    for (int i = 0; i < renderers.Length; i++)
        //    {
        //        renderers[i].material.color = m_Attributes.PlayerColor;
        //    }

        //    m_InputController.m_PlayerWeapon = weapon;
        //}

        public void RemoveWeapon()
        {
            //SetWeapon(m_EmptyWeapon);
            m_WeaponHandler.SetWeapon(Instantiate(m_Attributes.PlayerStartingWeapon));
        }

        public void RemoveAbility()
        {
            m_AbilityHandler.SetAbility(Instantiate(m_Attributes.PlayerStartingAbility));
        }

        public void SetHealth(float plusHP)
        {
            m_Attributes.PlayerCurrentHealth = Mathf.Clamp(m_Attributes.PlayerCurrentHealth += plusHP, 0, m_Attributes.PlayerMaxHealth);
            if (m_Attributes.PlayerCurrentHealth <= 0 && !m_Attributes.PlayerDeath)
            {
                OnDeath();
            }
        }

        public float GetHealth()
        {
            return m_Attributes.PlayerCurrentHealth;
        }

        public void TakeDamage(float damage)
        {
            SetHealth(-damage);
        }

        public void SetEnergy(float plusEP)
        {
            m_Attributes.PlayerCurrentEnergy = Mathf.Clamp(m_Attributes.PlayerCurrentEnergy += plusEP, 0, m_Attributes.PlayerMaxEnergy);
        }

        public float GetEnergy()
        {
            return m_Attributes.PlayerCurrentEnergy;
        }

        public float GetPlayerVelocity()
        {
            return m_Attributes.PlayerVelocity;
        }

        public Transform GetWeaponTransform()
        {
            return m_WeaponTransform;
        }

        private void SetHealthUI()
        {
            m_HealthSlider.value = (m_Attributes.PlayerCurrentHealth / m_Attributes.PlayerMaxHealth) * 100f;
            m_HealthSliderFillImage.color = Color.Lerp(m_ZeroHealthSliderColor, m_FullHealthSliderColor, m_Attributes.PlayerCurrentHealth / m_Attributes.PlayerMaxHealth);
        }

        private void RegenAttributes()
        {
            m_Attributes.PlayerCurrentHealth = Mathf.Min((m_Attributes.PlayerCurrentHealth + (m_Attributes.PlayerCurrentRegenHealth * Time.deltaTime)), m_Attributes.PlayerMaxHealth);
            m_Attributes.PlayerCurrentEnergy = Mathf.Min((m_Attributes.PlayerCurrentEnergy + (m_Attributes.PlayerCurrentRegenEnergy * Time.deltaTime)), m_Attributes.PlayerMaxEnergy);
        }

        private void OnDeath()
        {
            m_Attributes.PlayerDeath = true;
            gameObject.SetActive(false);
        }
    }
}
