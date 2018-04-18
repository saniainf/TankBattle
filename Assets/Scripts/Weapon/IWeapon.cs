using UnityEngine;
using System.Collections;

namespace TankBattle
{
    public interface IWeapon
    {
        PlayerHandler playerHandler { get; set; }

        void FireButtonPress();
        void FireButtonHold();
        void FireButtonRelease();
    }
}