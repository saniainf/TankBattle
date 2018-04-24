using UnityEngine;
using System.Collections;

namespace TankBattle
{
    public interface IAbility
    {
        PlayerHandler m_PlayerHandler { get; set; }

        void AbilityButtonPress();
        void AbilityButtonHold();
        void AbilityButtonRelease();
    }
}