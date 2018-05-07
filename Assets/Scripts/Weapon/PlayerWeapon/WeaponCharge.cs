using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace TankBattle
{
    [CreateAssetMenu(fileName = "Weapon Charge", menuName = "Player/Weapon/Charge")]
    public class WeaponCharge : Weapon
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject turretPrefab;
        [SerializeField] private GameObject projectilePrefab;
        [SerializeField] private Canvas chargeCanvas;
        [SerializeField] private Slider chargeSlider;
        [SerializeField] private Transform fireTransform;
        [SerializeField] private ParticleSystem castBar;

        [Header("Property")]
        [SerializeField] private float minLaunchForce = 15f;
        [SerializeField] private float maxLaunchForce = 30f;
        [SerializeField] private float maxChargeTime = 0.75f;
        [SerializeField] private float atackSpeed = 0.5f;
        [SerializeField] private float energyCost = 10f;

        [SerializeField] private LayerMask m_ActiveLayers;
        [SerializeField] private ParticleSystem m_ExplosionParticles;
        [SerializeField] private AudioSource m_ExplosionAudio;
        [SerializeField] private float m_MaxDamage = 50f;
        [SerializeField] private float m_ExplosionForce = 500f;
        [SerializeField] private float m_MaxLifeTime = 3f;
        [SerializeField] private float m_ExplosionRadius = 5f;

        private bool charge = false;
        private bool reload = false;
        private float currentLaunchForce;
        private float chargeSpeed;
        private float reloadTime = 0f;

        public override void SetupWeapon(Transform weaponTransform)
        {
            chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
            currentLaunchForce = minLaunchForce;
            //chargeSlider.value = chargeSlider.minValue;
        }

        public override void WeaponButtonHold()
        {
            castBar.Play();
        }
    }
}