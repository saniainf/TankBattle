using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public class PlayerInputController : MonoBehaviour
    {
        public IWeapon playerWeapon;

        private PlayerAttributes playerAttributes;
        private PlayerMovement playerMovement;

        private string movementAxisName;
        private string turnAxisName;

        private float movementInputValue;
        private float turnInputValue;

        private string fireButton;

        private void Awake()
        {
            playerAttributes = gameObject.GetComponent<PlayerAttributes>();
            playerMovement = gameObject.GetComponent<PlayerMovement>();
        }

        private void OnEnable()
        {
            movementInputValue = 0f;
            turnInputValue = 0f;
        }

        void Start()
        {
            movementAxisName = "Vertical" + playerAttributes.PlayerNumber;
            turnAxisName = "Horizontal" + playerAttributes.PlayerNumber;
            fireButton = "Fire" + playerAttributes.PlayerNumber;
        }

        void Update()
        {
            movementInputValue = Input.GetAxis(movementAxisName);
            turnInputValue = Input.GetAxis(turnAxisName);
            playerMovement.MovePlayer(movementInputValue);
            playerMovement.TurnPlayer(turnInputValue);

            if (Input.GetButtonDown(fireButton))
            {
                playerWeapon.FireButtonPress();
            }
            else if (Input.GetButton(fireButton))
            {
                playerWeapon.FireButtonHold();
            }
            else if (Input.GetButtonUp(fireButton))
            {
                playerWeapon.FireButtonRelease();
            }
        }


    }
}