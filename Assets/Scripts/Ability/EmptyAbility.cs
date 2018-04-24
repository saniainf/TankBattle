using UnityEngine;
using System.Collections;

namespace TankBattle
{
    public class EmptyAbility : MonoBehaviour, IAbility
    {
        public PlayerHandler m_PlayerHandler { get; set; }

        public void AbilityButtonHold() { }

        public void AbilityButtonPress() { }

        public void AbilityButtonRelease() { }
    }
}