using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankBattle
{
    public class PlayerMovement : MonoBehaviour
    {
        public PlayerHandler m_PlayerHandler;

        private Vector3 previousPosition;

        public void MovePlayer(float power)
        {
            // TODO заменить реализацию на чтото более вменяемое
            Vector3 movement = m_PlayerHandler.transform.forward * power * m_PlayerHandler.m_Attributes.PlayerCurrentSpeed * Time.fixedDeltaTime;
            m_PlayerHandler.m_Rigidbody.MovePosition(m_PlayerHandler.m_Rigidbody.position + movement);

            m_PlayerHandler.m_Attributes.PlayerVelocity = Mathf.Sign(power) * ((m_PlayerHandler.transform.position - previousPosition).magnitude / Time.deltaTime);
            previousPosition = m_PlayerHandler.transform.position;
        }

        public void TurnPlayer(float power)
        {
            // TODO заменить реализацию на чтото более вменяемое
            float turn = power * m_PlayerHandler.m_Attributes.PlayerCurrentTurnSpeed * Time.fixedDeltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            m_PlayerHandler.m_Rigidbody.MoveRotation(m_PlayerHandler.m_Rigidbody.rotation * turnRotation);
        }
    }
}
