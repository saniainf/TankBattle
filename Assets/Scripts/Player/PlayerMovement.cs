﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public class PlayerMovement : MonoBehaviour
    {
        private PlayerAttributes playerAttributes;
        private Transform playerTransform;
        private Rigidbody playerRigidbody;

        private Vector3 previousPosition;

        private void Awake()
        {
            playerAttributes = gameObject.GetComponent<PlayerAttributes>();
            playerTransform = gameObject.GetComponent<Transform>();
            playerRigidbody = gameObject.GetComponent<Rigidbody>();
        }

        public void MovePlayer(float power)
        {
            // TODO заменить реализацию на чтото более вменяемое
            Vector3 movement = playerTransform.forward * power * playerAttributes.PlayerCurrentSpeed * Time.fixedDeltaTime;
            playerRigidbody.MovePosition(playerRigidbody.position + movement);

            playerAttributes.PlayerVelocity = Mathf.Sign(power) * ((playerTransform.position - previousPosition).magnitude / Time.deltaTime);
            previousPosition = playerTransform.position;
        }

        public void TurnPlayer(float power)
        {
            // TODO заменить реализацию на чтото более вменяемое
            float turn = power * playerAttributes.PlayerCurrentTurnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            playerRigidbody.MoveRotation(playerRigidbody.rotation * turnRotation);
        }
    }
}
