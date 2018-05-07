using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public abstract class Weapon : ScriptableObject
    {
        [HideInInspector] public PlayerHandler m_PlayerHandler;

        public virtual void SetupWeapon(Transform weaponTransform) { }

        public virtual void WeaponButtonPress() { }

        public virtual void WeaponButtonHold() { }

        public virtual void WeaponButtonRelease() { }

        public virtual void OnImpact(WeaponProjectile abilityProjectile, Collider[] other) { }
    }
}