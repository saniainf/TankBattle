using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TankBattle
{
    public class ProjectileBehaviour : MonoBehaviour
    {
        [HideInInspector] public WeaponProjectile m_WeaponProjectile;
        [HideInInspector] public float m_CollisionRadius = 0.1f;
        [HideInInspector] public LayerMask m_ActiveLayers;

        private Collider[] colliders;

        private void FixedUpdate()
        {
            colliders = Physics.OverlapSphere(transform.position, m_CollisionRadius, m_ActiveLayers);
            if (colliders.Length > 0)
            {
                m_WeaponProjectile.OnImpact(this, colliders);
            }
        }
    }
}