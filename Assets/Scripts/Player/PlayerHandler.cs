using UnityEngine;
using UnityEngine.UI;

namespace TankBattle
{
    public class PlayerHandler : MonoBehaviour
    {
        [Header("PlayerComponents")]
        public Rigidbody m_Rigidbody;
        public PlayerAttributes m_Attributes;
        public PlayerInputController m_InputController;
        public PlayerMovement m_Movement;

        [Header("PlayerWeapon")]
        public Transform m_WeaponTransform;
        public GameObject m_EmptyWeapon;

        [Header("Player Health")]
        public Canvas m_HealthCanvas;
        public Slider m_HealthSlider;
        public Image m_HealthSliderFillImage;
        public Color m_FullHealthSliderColor = Color.green;
        public Color m_ZeroHealthSliderColor = Color.red;

        private GameObject currentWeapon;

        void Update()
        {
            SetHealthUI();
            RegenAttributes();
        }

        public void ResetPlayer()
        {
            m_Attributes.ResetAttributes();
            RemoveWeapon();
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

        public void SetWeapon(GameObject weaponPrefab)
        {
            if (currentWeapon != null)
                Destroy(currentWeapon);

            currentWeapon = Instantiate(weaponPrefab, m_WeaponTransform);

            IWeapon weapon = currentWeapon.GetComponent<IWeapon>();
            weapon.m_PlayerHandler = this;

            MeshRenderer[] renderers = currentWeapon.GetComponents<MeshRenderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = m_Attributes.PlayerColor;
            }

            m_InputController.m_PlayerWeapon = weapon;
        }

        public void RemoveWeapon()
        {
            SetWeapon(m_EmptyWeapon);
        }

        public void SetAbillity()
        {

        }

        public void GetAbillity()
        {

        }

        public void SetModificator()
        {

        }

        public void GetModificator()
        {

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

        public void SetEnergy(float plusEnergy)
        {
            m_Attributes.PlayerCurrentEnergy = Mathf.Clamp(m_Attributes.PlayerCurrentEnergy += plusEnergy, 0, m_Attributes.PlayerMaxEnergy);
        }

        public float GetEnergy()
        {
            return m_Attributes.PlayerCurrentEnergy;
        }

        public float GetPlayerVelocity()
        {
            return m_Attributes.PlayerVelocity;
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
