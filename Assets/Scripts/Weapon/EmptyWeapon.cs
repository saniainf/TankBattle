using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public class EmptyWeapon : MonoBehaviour, IWeapon
    {
        public PlayerHandler playerHandler { get; set; }

        public void FireButtonPress() { }

        public void FireButtonHold() { }

        public void FireButtonRelease() { }
    }
}
