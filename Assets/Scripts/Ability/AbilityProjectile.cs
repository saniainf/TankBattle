using UnityEngine;
using System.Collections;

namespace TankBattle
{
    public class AbilityProjectile : MonoBehaviour
    {
        [HideInInspector] public Ability m_ParentAbility;
        [HideInInspector] public float m_CollisionRadius = 0.5f;
        public LayerMask m_ActiveLayers;

        private Collider[] colliders;

        private void FixedUpdate()
        {
            colliders = Physics.OverlapSphere(transform.position, m_CollisionRadius, m_ActiveLayers);
            if (colliders.Length > 0)
            {
                m_ParentAbility.OnImpact(this, colliders);
            }
        }
    }
}