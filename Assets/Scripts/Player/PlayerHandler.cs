using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TankBattle
{
    public class PlayerHandler : MonoBehaviour
    {
        public PlayerAttributes playerAttributes;
        public PlayerMovement playerMovement;

        [Header("Player Health")]
        public Canvas playerCanvasHealth;
        public Slider healthSlider;
        public Image healthSliderImage;
        public Color fullHealthColor = Color.green;
        public Color zeroHealthColor = Color.red;

        void Start()
        {
            
        }

        void Update()
        {
            SetHealthUI();
        }

        public void ResetPlayer()
        {
            playerAttributes.PlayerCurrentHealth = playerAttributes.PlayerStartingHealth;
        }

        public void EnablePlayer()
        {
            playerMovement.enabled = true;
            playerCanvasHealth.enabled = true;
            // enable weapon
        }

        public void DisablePlayer()
        {
            playerMovement.enabled = false;
            playerCanvasHealth.enabled = false;
            // disable weapon
        }

        private void SetHealthUI()
        {
            healthSlider.value = (playerAttributes.PlayerCurrentHealth / playerAttributes.PlayerStartingHealth) * 100f;
            healthSliderImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, playerAttributes.PlayerCurrentHealth / playerAttributes.PlayerStartingHealth);
        }
    }
}
