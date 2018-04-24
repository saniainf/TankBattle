using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public class GunTurretProjectile : MonoBehaviour
    {
        public LayerMask m_ActiveLayers;
        public ParticleSystem m_ExplosionParticles;
        public AudioSource m_ExplosionAudio;
        public float m_MinDamage = 20f;
        public float m_MaxDamage = 30f;
        public float m_ExplosionForce = 200f;
        public float m_MaxLifeTime = 3f;

        private Transform projectileTransform;

        private void Start()
        {
            projectileTransform = gameObject.GetComponent<Transform>();
            Destroy(gameObject, m_MaxLifeTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            Rigidbody targetRigitbody;
            PlayerHandler playerHandler;

            if (m_ActiveLayers.HasLayer(other.gameObject.layer))
            {
                targetRigitbody = other.gameObject.GetComponent<Rigidbody>();
                if (!targetRigitbody)
                    return;

                targetRigitbody.AddForce(projectileTransform.forward * m_ExplosionForce);

                playerHandler = other.gameObject.GetComponent<PlayerHandler>();
                if (!playerHandler)
                    return;

                playerHandler.TakeDamage(Random.Range(m_MinDamage, m_MaxDamage));
            }

            m_ExplosionParticles.gameObject.transform.parent = null;
            m_ExplosionParticles.Play();
            m_ExplosionAudio.Play();
            Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
            Destroy(gameObject);
        }
    }
}
