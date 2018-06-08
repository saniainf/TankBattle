using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    /// <summary>
    /// Базовый класс для оружия игрока
    /// </summary>
    public abstract class Weapon : ScriptableObject
    {
        [Header("Base attributes")]
        [SerializeField]
        protected GameObject weaponPrefab;
        [SerializeField] protected WeaponProjectile projectileSO;
        [SerializeField] protected Transform fireTransform;


        protected PlayerHandler playerHandler;
        protected Transform weaponTransform;
        protected GameObject weaponInstance;

        /// <summary>
        /// Конструктор оружия
        /// </summary>
        /// <param name="playerHandler">handle игрока оружия</param>
        /// <param name="weaponTransform">transform положения оружия</param>
        public virtual void SetupWeapon(PlayerHandler playerHandler, Transform weaponTransform)
        {
            this.playerHandler = playerHandler;
            this.weaponTransform = weaponTransform;
            if (weaponPrefab != null)
            {
                weaponInstance = Instantiate(weaponPrefab, weaponTransform);
                MeshRenderer renderer = weaponInstance.GetComponent<MeshRenderer>();
                renderer.material.color = playerHandler.m_PlayerAttributes.PlayerColor;
            }
            if (fireTransform != null)
            {
                fireTransform = Instantiate(fireTransform, weaponInstance.transform);
            }
        }

        public virtual void RemoveWeapon()
        {
            if (weaponInstance != null)
                Destroy(weaponInstance);

            Destroy(this);
        }

        public virtual void Update() { }

        public virtual void WeaponButtonPress() { }

        public virtual void WeaponButtonHold() { }

        public virtual void WeaponButtonRelease() { }
    }
}