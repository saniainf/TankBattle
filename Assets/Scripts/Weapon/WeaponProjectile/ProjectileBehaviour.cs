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
        [HideInInspector] public ProjectileType m_ProjectileType = ProjectileType.PROJECTILE_NONE;
        private WeaponProjectile weaponProjectile;
        [HideInInspector] public LayerMask m_ActiveLayers;
        [HideInInspector] public float m_CollisionOverlapSphereRadius = 0.1f;
        [HideInInspector] public float m_ImpactOverlapSphereRadius = 0.1f;

        private Collider[] collisionColliders;
        private Collider[] impactColliders;

        /// <summary>
        /// Настройка поведения снаряда
        /// </summary>
        public void SetupProjectileBehaviour(WeaponProjectile weaponProjectile, ProjectileCallback projectileCallback = ProjectileCallback.PROJECTILE_CALLBACK_NONE)
        {
            this.weaponProjectile = weaponProjectile;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (m_ProjectileType.HasFlag(ProjectileType.PROJECTILE_COLLISON_ON_TRIGGER))
            {
                weaponProjectile.OnCollide(this, new Collider[] { other });
            }

            if (m_ProjectileType.HasFlag(ProjectileType.PROJECTILE_IMPACT_ON_TRIGGER))
            {
                weaponProjectile.OnImpact(this, new Collider[] { other });
            }
        }

        private void FixedUpdate()
        {
            if (!m_ProjectileType.HasFlag(ProjectileType.PROJECTILE_COLLISON_ON_TRIGGER))
                CheckCollision();

            if (!m_ProjectileType.HasFlag(ProjectileType.PROJECTILE_IMPACT_ON_TRIGGER))
                CheckImpact();
        }

        private void CheckCollision()
        {
            if (m_ProjectileType.HasFlag(ProjectileType.PROJECTILE_COLLISION_ON_OVERLAPE_SPHERE))
            {
                collisionColliders = Physics.OverlapSphere(transform.position, m_CollisionOverlapSphereRadius, m_ActiveLayers);
                if (collisionColliders.Length > 0)
                    weaponProjectile.OnCollide(this, collisionColliders);
            }
        }

        private void CheckImpact()
        {
            if (m_ProjectileType.HasFlag(ProjectileType.PROJECTILE_IMPACT_ON_OVERLAPE_SPHERE))
            {
                impactColliders = Physics.OverlapSphere(transform.position, m_ImpactOverlapSphereRadius, m_ActiveLayers);
                if (impactColliders.Length > 0)
                    weaponProjectile.OnImpact(this, impactColliders);

            }
        }
    }
}