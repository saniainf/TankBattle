using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    [System.Serializable]
    public class PlayerMovement
    {
        private PlayerHandler playerHandler;
        private Vector3 previousPosition;

        public PlayerMovement(PlayerHandler playerHandler)
        {
            this.playerHandler = playerHandler;
        }

        public void MovePlayer(float power)
        {
            // TODO заменить реализацию на чтото более вменяемое
            Vector3 movement = playerHandler.transform.forward * power * playerHandler.m_PlayerAttributes.PlayerCurrentSpeed * Time.fixedDeltaTime;
            playerHandler.m_Rigidbody.MovePosition(playerHandler.m_Rigidbody.position + movement);

            playerHandler.m_PlayerAttributes.PlayerVelocity = Mathf.Sign(power) * ((playerHandler.transform.position - previousPosition).magnitude / Time.deltaTime);
            previousPosition = playerHandler.transform.position;
        }

        public void TurnPlayer(float power)
        {
            // TODO заменить реализацию на чтото более вменяемое
            float turn = power * playerHandler.m_PlayerAttributes.PlayerCurrentTurnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            playerHandler.m_Rigidbody.MoveRotation(playerHandler.m_Rigidbody.rotation * turnRotation);
        }
    }
}
