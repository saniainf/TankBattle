using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public class PlayerInputController : MonoBehaviour
    {
        private PlayerHandler playerHandler;

        private string movementAxisName;
        private string turnAxisName;
        private string weaponButtonName;
        private string abilityButtonName;

        private float movementInputValue;
        private float turnInputValue;

        private void Awake()
        {
            playerHandler = gameObject.GetComponent<PlayerHandler>();
        }

        private void OnEnable()
        {
            movementInputValue = 0f;
            turnInputValue = 0f;
        }

        void Start()
        {
            movementAxisName = "Vertical" + playerHandler.m_PlayerAttributes.PlayerNumber;
            turnAxisName = "Horizontal" + playerHandler.m_PlayerAttributes.PlayerNumber;
            weaponButtonName = "Fire" + playerHandler.m_PlayerAttributes.PlayerNumber;
            abilityButtonName = "Ability" + playerHandler.m_PlayerAttributes.PlayerNumber;
        }

        private void FixedUpdate()
        {
            movementInputValue = Input.GetAxis(movementAxisName);
            turnInputValue = Input.GetAxis(turnAxisName);
            playerHandler.m_Movement.MovePlayer(movementInputValue);
            playerHandler.m_Movement.TurnPlayer(turnInputValue);
        }

        void Update()
        {
            if (Input.GetButtonDown(weaponButtonName))
            {
                playerHandler.m_PlayerWeaponHandler.WeaponButtonPress();
            }
            else if (Input.GetButton(weaponButtonName))
            {
                playerHandler.m_PlayerWeaponHandler.WeaponButtonHold();
            }
            else if (Input.GetButtonUp(weaponButtonName))
            {
                playerHandler.m_PlayerWeaponHandler.WeaponButtonRelease();
            }

            if (Input.GetButtonDown(abilityButtonName))
            {
                playerHandler.m_PlayerAbilityHandler.AbilityButtonPress();
            }
            else if (Input.GetButton(abilityButtonName))
            {
                playerHandler.m_PlayerAbilityHandler.AbilityButtonHold();
            }
            else if (Input.GetButtonUp(abilityButtonName))
            {
                playerHandler.m_PlayerAbilityHandler.AbilityButtonRelease();
            }
        }


    }
}