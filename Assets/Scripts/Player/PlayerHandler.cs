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
        }

        public void ResetPlayer()
        {
            playerAttributes.PlayerCurrentHealth = playerAttributes.PlayerStartingHealth;
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

            MeshRenderer[] renderers = currentWeapon.GetComponents<MeshRenderer>();

            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = playerAttributes.PlayerColor;
            }

            playerInputController.playerWeapon = currentWeapon.GetComponent<IWeapon>();
        }

        public void SetAbillity()
        {

        }

        public void SetModificator()
        {

        }

        public void RemoveWeapon()
        {
            SetWeapon(EmptyWeapon);
        }

        private void SetHealthUI()
        {
            healthSlider.value = (playerAttributes.PlayerCurrentHealth / playerAttributes.PlayerStartingHealth) * 100f;
            healthSliderImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, playerAttributes.PlayerCurrentHealth / playerAttributes.PlayerStartingHealth);
        }
    }
}
