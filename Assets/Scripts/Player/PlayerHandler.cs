using UnityEngine;
using UnityEngine.UI;

namespace TankBattle
{
    public class PlayerHandler : MonoBehaviour
    {
        [Header("PlayerComponents")]
        public Rigidbody m_Rigidbody;
        [SerializeField] private PlayerAttributes playerAttributes;

        [HideInInspector] public PlayerAttributes m_PlayerAttributes;
        private PlayerInputController inputController;
        [HideInInspector] public PlayerAbilityHandler m_PlayerAbilityHandler;
        [HideInInspector] public PlayerWeaponHandler m_PlayerWeaponHandler;
        [HideInInspector] public PlayerMovement m_Movement;

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
            m_PlayerAttributes = Instantiate(playerAttributes);
            inputController = gameObject.GetComponent<PlayerInputController>();
            m_PlayerAbilityHandler = gameObject.GetComponent<PlayerAbilityHandler>();
            m_PlayerWeaponHandler = gameObject.GetComponent<PlayerWeaponHandler>();
            m_Movement = new PlayerMovement(this);

            m_PlayerAbilityHandler.m_PlayerHandler = this;
            m_PlayerWeaponHandler.m_PlayerHandler = this;
        }

        void Update()
        {
            SetHealthUI();
            RegenAttributes();
        }

        public void ResetPlayer()
        {
            m_PlayerAttributes.ResetAttributes();
            ResetWeapon();
            ResetAbility();
        }

        public void EnablePlayer()
        {
            inputController.enabled = true;
            m_HealthCanvas.enabled = true;
            m_Rigidbody.isKinematic = false;
        }

        public void DisablePlayer()
        {
            inputController.enabled = false;
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

        public void ResetWeapon()
        {
            m_PlayerWeaponHandler.SetWeapon(Instantiate(m_PlayerAttributes.PlayerStartingWeapon));
        }

        public void ResetAbility()
        {
            m_PlayerAbilityHandler.SetAbility(Instantiate(m_PlayerAttributes.PlayerStartingAbility));
        }

        public void SetHealth(float plusHP)
        {
            m_PlayerAttributes.PlayerCurrentHealth = Mathf.Clamp(m_PlayerAttributes.PlayerCurrentHealth += plusHP, 0, m_PlayerAttributes.PlayerMaxHealth);
            if (m_PlayerAttributes.PlayerCurrentHealth <= 0 && !m_PlayerAttributes.PlayerDeath)
            {
                OnDeath();
            }
        }

        public float GetHealth()
        {
            return m_PlayerAttributes.PlayerCurrentHealth;
        }

        public void TakeDamage(float damage)
        {
            SetHealth(-damage);
        }

        public void SetEnergy(float plusEP)
        {
            m_PlayerAttributes.PlayerCurrentEnergy = Mathf.Clamp(m_PlayerAttributes.PlayerCurrentEnergy += plusEP, 0, m_PlayerAttributes.PlayerMaxEnergy);
        }

        public float GetEnergy()
        {
            return m_PlayerAttributes.PlayerCurrentEnergy;
        }

        public float GetPlayerVelocity()
        {
            return m_PlayerAttributes.PlayerVelocity;
        }

        public Transform GetWeaponTransform()
        {
            return m_WeaponTransform;
        }

        private void SetHealthUI()
        {
            m_HealthSlider.value = (m_PlayerAttributes.PlayerCurrentHealth / m_PlayerAttributes.PlayerMaxHealth) * 100f;
            m_HealthSliderFillImage.color = Color.Lerp(m_ZeroHealthSliderColor, m_FullHealthSliderColor, m_PlayerAttributes.PlayerCurrentHealth / m_PlayerAttributes.PlayerMaxHealth);
        }

        private void RegenAttributes()
        {
            m_PlayerAttributes.PlayerCurrentHealth = Mathf.Min((m_PlayerAttributes.PlayerCurrentHealth + (m_PlayerAttributes.PlayerCurrentRegenHealth * Time.deltaTime)), m_PlayerAttributes.PlayerMaxHealth);
            m_PlayerAttributes.PlayerCurrentEnergy = Mathf.Min((m_PlayerAttributes.PlayerCurrentEnergy + (m_PlayerAttributes.PlayerCurrentRegenEnergy * Time.deltaTime)), m_PlayerAttributes.PlayerMaxEnergy);
        }

        private void OnDeath()
        {
            m_PlayerAttributes.PlayerDeath = true;
            gameObject.SetActive(false);
        }
    }
}
