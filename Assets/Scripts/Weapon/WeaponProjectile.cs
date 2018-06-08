using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace TankBattle
{
    /// <summary>
    /// Базовый класс для снаряда оружия
    /// </summary>
    public abstract class WeaponProjectile : ScriptableObject
    {
        [Header("Base attributes")]
        [SerializeField]
        protected GameObject projectilePrefab;
        [SerializeField] private float maxLifeTime = 5f;
        protected int playerNumber;
        protected GameObject projectileInstance;
        protected Transform fireTransform;

        /// <summary>
        /// Конструктор снаряда
        /// <para>создание экземпляра снаряда, установка времени дестроя</para>
        /// </summary>
        /// <param name="playerNumber">номер игрока</param>
        /// <param name="playerVelocity">скорость игрока из атрибутов</param>
        /// <param name="fireTransform">transform инициализации снаряда</param>
        /// <param name="projectileModificators">модификаторы снаряда</param>
        public virtual void SetupProjectile(int playerNumber, float playerVelocity, Transform fireTransform, ProjectileModificators projectileModificators = null)
        {
            if (projectileModificators == null)
            {
                projectileModificators = new ProjectileModificators() { { ProjectileModifierProperty.PROPERTY_NONE, 0f } };
            }
            this.playerNumber = playerNumber;
            this.fireTransform = fireTransform;
            if (projectilePrefab != null)
                projectileInstance = Instantiate(projectilePrefab, fireTransform.position, fireTransform.rotation);
            Destroy(projectileInstance, maxLifeTime);
            Destroy(this, maxLifeTime);
        }

        public virtual void OnImpact(ProjectileBehaviour projectileBehaviour, Collider[] other) { }

        public virtual void OnCollide(ProjectileBehaviour projectileBehaviour, Collider[] other) { }

        public virtual void ProjectileFixedUpdate(ProjectileBehaviour projectileBehaviour) { }

        public virtual void ProjectileOnImpact(ProjectileBehaviour projectileBehaviour) { }

        public virtual void RemoveProjectile()
        {
            if (projectileInstance != null)
                Destroy(projectileInstance);
            Destroy(this);
        }
    }
}