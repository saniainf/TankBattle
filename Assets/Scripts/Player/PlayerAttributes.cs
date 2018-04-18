using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public class PlayerAttributes : MonoBehaviour
    {
        public int PlayerNumber = 1;
        public Color PlayerColor = Color.gray;

        public float PlayerStartingSpeed = 12f;
        [HideInInspector] public float PlayerMaxSpeed;
        [HideInInspector] public float PlayerCurrentSpeed;

        public float PlayerStartingTurnSpeed = 180f;
        [HideInInspector] public float PlayerMaxTurnSpeed;
        [HideInInspector] public float PlayerCurrentTurnSpeed;

        public float PlayerStartingHealth = 100f;
        [HideInInspector] public float PlayerMaxHealth;
        /*[HideInInspector]*/ public float PlayerCurrentHealth;

        public float PlayerStartingRegenHealth = 1f;
        [HideInInspector] public float PlayerMaxRegenHealth;
        [HideInInspector] public float PlayerCurrentRegenHealth;

        public float PlayerStartingEnergy = 100f;
        [HideInInspector] public float PlayerMaxEnergy;
        /*[HideInInspector]*/ public float PlayerCurrentEnergy;

        public float PlayerStartingRegenEnergy = 5f;
        [HideInInspector] public float PlayerMaxRegenEnergy;
        [HideInInspector] public float PlayerCurrentRegenEnergy;

        public void ResetAttributes()
        {
            PlayerMaxSpeed = PlayerStartingSpeed;
            PlayerCurrentSpeed = PlayerStartingSpeed;

            PlayerMaxTurnSpeed = PlayerStartingTurnSpeed;
            PlayerCurrentTurnSpeed = PlayerStartingTurnSpeed;

            PlayerMaxHealth = PlayerStartingHealth;
            PlayerCurrentHealth = PlayerStartingHealth;

            PlayerMaxRegenHealth = PlayerStartingRegenHealth;
            PlayerCurrentRegenHealth = PlayerStartingRegenHealth;

            PlayerMaxEnergy = PlayerStartingEnergy;
            PlayerCurrentEnergy = PlayerStartingEnergy;

            PlayerMaxRegenEnergy = PlayerStartingRegenEnergy;
            PlayerCurrentRegenEnergy = PlayerStartingRegenEnergy;
        }
    }
}
