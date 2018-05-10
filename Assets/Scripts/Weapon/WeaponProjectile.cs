using UnityEngine;
using System.Collections;

namespace TankBattle
{
    public class WeaponProjectile : MonoBehaviour
    {
        [HideInInspector] public Weapon m_ParentWeapon;
        [HideInInspector] public float m_CollisionRadius = 0.1f;
        [HideInInspector] public LayerMask m_ActiveLayers;

        private Collider[] colliders;

        private void FixedUpdate()
        {
            colliders = Physics.OverlapSphere(transform.position, m_CollisionRadius, m_ActiveLayers);
            if (colliders.Length > 0)
            {
                m_ParentWeapon.OnImpact(this, colliders);
            }
        }
    }
}