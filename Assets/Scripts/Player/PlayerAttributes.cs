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
        [HideInInspector]public float PlayerCurrentSpeed = 12f;
        public float PlayerStartingTurnSpeed = 180f;
        [HideInInspector] public float PlayerCurrentTurnSpeed = 180f;
        public float PlayerStartingHealth = 100f;
        [HideInInspector] public float PlayerCurrentHealth = 100f;
        public float PlayerStartingEnergy = 100f;
        [HideInInspector] public float PlayerCurrentEnergy = 100f;
    }
}
