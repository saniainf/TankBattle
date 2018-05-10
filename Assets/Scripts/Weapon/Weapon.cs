using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public abstract class Weapon : ScriptableObject
    {
        [SerializeField] protected GameObject turretPrefab;
        [SerializeField] protected GameObject projectilePrefab;
        [SerializeField] protected Transform fireTransform;
        protected PlayerHandler playerHandler;
        protected Transform weaponTransform;
        protected GameObject weaponTurret;

        public virtual void SetupWeapon(PlayerHandler playerHandler, Transform weaponTransform)
        {
            this.playerHandler = playerHandler;
            this.weaponTransform = weaponTransform;
            if (turretPrefab != null)
            {
                weaponTurret = Instantiate(turretPrefab, weaponTransform);
                MeshRenderer renderer = weaponTurret.GetComponent<MeshRenderer>();
                renderer.material.color = playerHandler.m_PlayerAttributes.PlayerColor;
            }
            if (fireTransform != null)
            {
                fireTransform = Instantiate(fireTransform, weaponTurret.transform);
            }
        }

        public virtual void RemoveWeapon()
        {
            if (weaponTurret != null)
            {
                Destroy(weaponTurret);
            }
        }

        public virtual void Update() { }

        public virtual void WeaponButtonPress() { }

        public virtual void WeaponButtonHold() { }

        public virtual void WeaponButtonRelease() { }

        public virtual void OnImpact(WeaponProjectile weaponProjectile, Collider[] other) { }
    }
}