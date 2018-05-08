using UnityEngine;
using System.Collections;

namespace TankBattle
{
    public interface IWeapon
    {
        PlayerHandler m_PlayerHandler { get; set; }

        void FireButtonPress();
        void FireButtonHold();
        void FireButtonRelease();
    }
}