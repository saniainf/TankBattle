using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public class PlayerInputController : MonoBehaviour
    {
        [HideInInspector]public IWeapon m_PlayerWeapon;

        public PlayerHandler m_PlayerHandler;

        private string movementAxisName;
        private string turnAxisName;

        private float movementInputValue;
        private float turnInputValue;

        private string fireButton;

        private void OnEnable()
        {
            movementInputValue = 0f;
            turnInputValue = 0f;
        }

        void Start()
        {
            movementAxisName = "Vertical" + m_PlayerHandler.m_Attributes.PlayerNumber;
            turnAxisName = "Horizontal" + m_PlayerHandler.m_Attributes.PlayerNumber;
            fireButton = "Fire" + m_PlayerHandler.m_Attributes.PlayerNumber;
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
            if (Input.GetButtonDown(fireButton))
            {
                m_PlayerWeapon.FireButtonPress();
            }
            else if (Input.GetButton(fireButton))
            {
                m_PlayerWeapon.FireButtonHold();
            }
            else if (Input.GetButtonUp(fireButton))
            {
                m_PlayerWeapon.FireButtonRelease();
            }
        }


    }
}