using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public class PlayerInputController : MonoBehaviour
    {
        //[HideInInspector]public IWeapon m_PlayerWeapon;

        public PlayerHandler m_PlayerHandler;

        private string movementAxisName;
        private string turnAxisName;
        private string weaponButtonName;
        private string abilityButtonName;

        private float movementInputValue;
        private float turnInputValue;

        private void OnEnable()
        {
            movementInputValue = 0f;
            turnInputValue = 0f;
        }

        void Start()
        {
            movementAxisName = "Vertical" + m_PlayerHandler.m_Attributes.PlayerNumber;
            turnAxisName = "Horizontal" + m_PlayerHandler.m_Attributes.PlayerNumber;
            weaponButtonName = "Fire" + m_PlayerHandler.m_Attributes.PlayerNumber;
            abilityButtonName = "Ability" + m_PlayerHandler.m_Attributes.PlayerNumber;
        }

        private void FixedUpdate()
        {
            movementInputValue = Input.GetAxis(movementAxisName);
            turnInputValue = Input.GetAxis(turnAxisName);
            m_PlayerHandler.m_Movement.MovePlayer(movementInputValue);
            m_PlayerHandler.m_Movement.TurnPlayer(turnInputValue);
        }

        void Update()
        {
            if (Input.GetButtonDown(weaponButtonName))
            {
                //m_PlayerWeapon.FireButtonPress();
                m_PlayerHandler.m_WeaponHandler.WeaponButtonPress();
            }
            else if (Input.GetButton(weaponButtonName))
            {
                //m_PlayerWeapon.FireButtonHold();
                m_PlayerHandler.m_WeaponHandler.WeaponButtonHold();
            }
            else if (Input.GetButtonUp(weaponButtonName))
            {
                //m_PlayerWeapon.FireButtonRelease();
                m_PlayerHandler.m_WeaponHandler.WeaponButtonRelease();
            }

            if (Input.GetButtonDown(abilityButtonName))
            {
                m_PlayerHandler.m_AbilityHandler.AbilityButtonPress();
            }
            else if (Input.GetButton(abilityButtonName))
            {
                m_PlayerHandler.m_AbilityHandler.AbilityButtonHold();
            }
            else if (Input.GetButtonUp(abilityButtonName))
            {
                m_PlayerHandler.m_AbilityHandler.AbilityButtonRelease();
            }
        }


    }
}