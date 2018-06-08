using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TankBattle
{
    /// <summary>
    /// Компонент снарядов с калбэком событий
    /// </summary>
    public class ProjectileBehaviour : MonoBehaviour
    {
        private ProjectileCallback projectileCallback;
        private WeaponProjectile weaponProjectile;

        /// <summary>
        /// Настройка поведения снаряда
        /// </summary>
        public void SetupProjectileBehaviour(WeaponProjectile weaponProjectile, Vector3 velocity, ProjectileCallback projectileCallback = ProjectileCallback.PROJECTILE_CALLBACK_NONE)
        {
            this.weaponProjectile = weaponProjectile;
            this.projectileCallback = projectileCallback;
            gameObject.GetComponent<Rigidbody>().velocity = velocity;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (projectileCallback.HasFlag(ProjectileCallback.PROJECTILE_CALLBACK_ON_IMPACT))
            {
                weaponProjectile.ProjectileOnImpact(this, other);
            }
        }

        private void FixedUpdate()
        {
            if (!projectileCallback.HasFlag(ProjectileCallback.PROJECTILE_CALLBACK_FIXED))
                return;
            weaponProjectile.ProjectileFixedUpdate(this);
        }
    }
}