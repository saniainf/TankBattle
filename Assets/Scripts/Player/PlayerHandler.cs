using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle
{
    public class PlayerHandler : MonoBehaviour
    {
        public GameObject EmptyWeapon;
        public Transform weaponTransform;

        [Header("Player Health")]
        public Canvas playerCanvasHealth;
        public Slider healthSlider;
        public Image healthSliderImage;
        public Color fullHealthColor = Color.green;
        public Color zeroHealthColor = Color.red;

        private Rigidbody playerRigidbody;
        private PlayerAttributes playerAttributes;
        private PlayerInputController playerInputController;

        private GameObject currentWeapon;

        private void Awake()
        {
            playerAttributes = gameObject.GetComponent<PlayerAttributes>();
            playerRigidbody = gameObject.GetComponent<Rigidbody>();
            playerInputController = gameObject.GetComponent<PlayerInputController>();
        }

        void Update()
        {
            SetHealthUI();
            regenAttributes();
        }

        public void ResetPlayer()
        {
            playerAttributes.ResetAttributes();
            RemoveWeapon();
        }

        public void EnablePlayer()
        {
            playerInputController.enabled = true;
            playerCanvasHealth.enabled = true;
            playerRigidbody.isKinematic = false;
        }

        public void DisablePlayer()
        {
            playerInputController.enabled = false;
            playerCanvasHealth.enabled = false;
            playerRigidbody.isKinematic = true;
        }

        public void SetWeapon(GameObject weaponPrefab)
        {
            if (currentWeapon != null)
                Destroy(currentWeapon);

            currentWeapon = Instantiate(weaponPrefab, weaponTransform);

            IWeapon weapon = currentWeapon.GetComponent<IWeapon>();
            weapon.playerHandler = this;

            MeshRenderer[] renderers = currentWeapon.GetComponents<MeshRenderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = playerAttributes.PlayerColor;
            }

            playerInputController.playerWeapon = weapon;
        }

        public void RemoveWeapon()
        {
            SetWeapon(EmptyWeapon);
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

        public void SetHealth(float hp)
        {
            playerAttributes.PlayerCurrentHealth = Mathf.Clamp(playerAttributes.PlayerCurrentHealth += hp, 0, playerAttributes.PlayerMaxHealth);
            if (playerAttributes.PlayerCurrentHealth <= 0 && !playerAttributes.PlayerDeath)
            {
                OnDeath();
            }
        }

        public float GetHealth()
        {
            return playerAttributes.PlayerCurrentHealth;
        }

        public void TakeDamage(float damage)
        {
            SetHealth(-damage);
        }

        public void SetEnergy(float energy)
        {
            playerAttributes.PlayerCurrentEnergy = Mathf.Clamp(playerAttributes.PlayerCurrentEnergy += energy, 0, playerAttributes.PlayerMaxEnergy);
        }

        public float GetEnergy()
        {
            return playerAttributes.PlayerCurrentEnergy;
        }

        public float GetPlayerVelocity()
        {
            return playerAttributes.PlayerVelocity;
        }

        private void SetHealthUI()
        {
            healthSlider.value = (playerAttributes.PlayerCurrentHealth / playerAttributes.PlayerMaxHealth) * 100f;
            healthSliderImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, playerAttributes.PlayerCurrentHealth / playerAttributes.PlayerMaxHealth);
        }

        private void regenAttributes()
        {
            playerAttributes.PlayerCurrentHealth = Mathf.Min((playerAttributes.PlayerCurrentHealth + (playerAttributes.PlayerCurrentRegenHealth * Time.deltaTime)), playerAttributes.PlayerMaxHealth);
            playerAttributes.PlayerCurrentEnergy = Mathf.Min((playerAttributes.PlayerCurrentEnergy + (playerAttributes.PlayerCurrentRegenEnergy * Time.deltaTime)), playerAttributes.PlayerMaxEnergy);
        }

        private void OnDeath()
        {
            playerAttributes.PlayerDeath = true;
            gameObject.SetActive(false);
        }
    }
}
