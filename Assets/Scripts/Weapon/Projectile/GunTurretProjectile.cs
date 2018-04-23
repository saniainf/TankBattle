using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public class GunTurretProjectile : MonoBehaviour
    {
        public LayerMask m_LayerPlayers;
        public ParticleSystem m_ExplosionParticles;
        public AudioSource m_ExplosionAudio;
        public float m_MinDamage = 70f;
        public float m_MaxDamage = 100f;
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
            if (other.gameObject.layer == 8 || other.gameObject.layer == 9)
            {
                Rigidbody targetRigitbody;
                PlayerHandler playerHandler;

                targetRigitbody = other.gameObject.GetComponent<Rigidbody>();
                if (targetRigitbody)
                {
                    targetRigitbody.AddForce(projectileTransform.forward * m_ExplosionForce);
                }

                playerHandler = other.gameObject.GetComponent<PlayerHandler>();
                if (playerHandler)
                {
                    float damage = Random.Range(m_MinDamage, m_MaxDamage);
                    playerHandler.TakeDamage(damage);
                }

                m_ExplosionParticles.gameObject.transform.parent = null;
                m_ExplosionParticles.Play();
                m_ExplosionAudio.Play();
                Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.main.duration);
                Destroy(gameObject);
            }
        }
    }
}
