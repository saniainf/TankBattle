using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public class BonusSpawner : MonoBehaviour
    {
        public Weapon weapon;
        public GameObject BonusModel;
        public float ReloadTimer = 3.0f;
        public float TransformY = 0.3f;
        public float TurnSpeed = 1.0f;

        private GameObject bonus;
        private float timer;
        private bool reload;


        // Use this for initialization
        void Start()
        {
            bonus = Instantiate(BonusModel, new Vector3(transform.position.x, transform.position.y + TransformY, transform.position.z), transform.rotation, transform);
            reload = false;
            timer = ReloadTimer;
        }

        // Update is called once per frame
        void Update()
        {
            if (reload)
            {
                timer -= Time.deltaTime;
                if (timer < 0)
                {
                    bonus.SetActive(true);
                    reload = false;
                    timer = ReloadTimer;
                }
            }
            else
            {
                Quaternion turnRotation = Quaternion.Euler(0, TurnSpeed, 0);
                bonus.transform.rotation = bonus.transform.rotation * turnRotation;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            //ModifiersManager m_TankModifier;

            //if (m_TankModifier = other.gameObject.GetComponent<ModifiersManager>())
            //{
            //    m_TankModifier.AddModifier(new ModifierShield());
            //    gameObject.SetActive(false);
            //}

            //if (!reload)
            //{
            //    PlayerHandler playerHandler;
            //    if (playerHandler = other.gameObject.GetComponent<PlayerHandler>())
            //    {
            //        bonus.SetActive(false);
            //        reload = true;
            //        playerHandler.SetWeapon(BonusPrefab);
            //    }
            //}

            if (!reload)
            {
                PlayerHandler playerHandler;
                if (playerHandler = other.gameObject.GetComponent<PlayerHandler>())
                {
                    playerHandler.m_PlayerWeaponHandler.SetWeapon(Instantiate(weapon));
                }
            }
        }
    }
}