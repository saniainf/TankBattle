using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerAttributes playerAttributes;
        private Transform playerTransform;
        private Rigidbody playerRigidbody;

        private string movementAxisName;
        private string turnAxisName;

        private float movementInputValue;
        private float turnInputValue;

        private void Awake()
        {
            playerAttributes = gameObject.GetComponent<PlayerAttributes>();
            playerTransform = gameObject.GetComponent<Transform>();
            playerRigidbody = gameObject.GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            movementInputValue = 0f;
            turnInputValue = 0f;
            playerRigidbody.isKinematic = false;
        }

        void Start()
        {
            movementAxisName = "Vertical" + playerAttributes.PlayerNumber;
            turnAxisName = "Horizontal" + playerAttributes.PlayerNumber;
        }

        private void FixedUpdate()
        {
            // TODO заменить реализацию на чтото более вменяемое
            MovePlayer();
            TurnPlayer();
        }

        void Update()
        {
            movementInputValue = Input.GetAxis(movementAxisName);
            turnInputValue = Input.GetAxis(turnAxisName);
        }

        private void OnDisable()
        {
            playerRigidbody.isKinematic = true;
        }

        private void MovePlayer()
        {
            Vector3 movement = playerTransform.forward * movementInputValue * playerAttributes.PlayerCurrentSpeed * Time.fixedDeltaTime;
            playerRigidbody.MovePosition(playerRigidbody.position + movement);
        }

        private void TurnPlayer()
        {
            float turn = turnInputValue * playerAttributes.PlayerCurrentTurnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            playerRigidbody.MoveRotation(playerRigidbody.rotation * turnRotation);
        }
    }
}
