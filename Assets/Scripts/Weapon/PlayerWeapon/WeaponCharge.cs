using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace TankBattle
{
    [CreateAssetMenu(fileName = "Weapon Charge", menuName = "Player/Weapon/Charge")]
    public class WeaponCharge : Weapon
    {
        [Header("Attributes")]
        [SerializeField]
        private float atackSpeed = 0.2f;
        [SerializeField] private float energyCost = 25f;
        [SerializeField] private float minDamageMultiplier = 1.0f;
        [SerializeField] private float maxDamageMultiplier = 1.7f;
        [SerializeField] private float maxChargeTime = 0.75f;

        [Header("Effects")]
        [SerializeField]
        private ParticleSystem projectileExplosionEffect;
        [SerializeField] private ParticleSystem chargeEffect;

        private bool reload = false;
        private float reloadTime = 0f;
        private bool charge = false;
        private float currentDamageMultiplier;
        private float chargeSpeed;

        public override void SetupWeapon(PlayerHandler playerHandler, Transform weaponTransform)
        {
            base.SetupWeapon(playerHandler, weaponTransform);

            chargeEffect = Instantiate(chargeEffect, fireTransform);
            ParticleSystem.MainModule chargeEffectMain = chargeEffect.main;
            chargeEffectMain.duration = maxChargeTime;
            chargeEffectMain.startLifetime = maxChargeTime;
        }

        public override void Update()
        {
            if (reload)
                reloadTime += Time.deltaTime;

            if (reloadTime > atackSpeed && reload)
            {
                reloadTime -= atackSpeed;
                reload = false;
            }
        }

        public override void WeaponButtonHold()
        {
            if (!charge && !reload && playerHandler.GetEnergy() >= energyCost)
            {
                playerHandler.SetEnergy(-energyCost);
                chargeSpeed = (maxDamageMultiplier - minDamageMultiplier) / maxChargeTime;
                currentDamageMultiplier = minDamageMultiplier;
                chargeEffect.Play();
                charge = true;
            }

            if (charge && !reload)
            {
                currentDamageMultiplier += chargeSpeed * Time.deltaTime;

                if (currentDamageMultiplier >= maxDamageMultiplier)
                {
                    currentDamageMultiplier = maxDamageMultiplier;
                    Fire();
                }
            }
        }

        public override void WeaponButtonRelease()
        {
            if (charge && !reload)
            {
                Fire();
            }
        }

        private void Fire()
        {
            chargeEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            charge = false;
            reload = true;
            ProjectileModificators projectileModificators = new ProjectileModificators()
            {
                { ProjectileModifierProperty.PROPERTY_DAMAGE_MULTIPLIER, currentDamageMultiplier }
            };
            Instantiate(projectileSO).SetupProjectile(playerHandler.GetPlayerNumber(), playerHandler.GetPlayerVelocity(), fireTransform, projectileModificators);
        }
    }
}