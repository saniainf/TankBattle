using System;
using UnityEngine;

namespace TankBattle
{
    [Serializable]
    public class PlayerManager
    {
        public Color m_PlayerColor;
        public Transform m_SpawnPoint;
        [HideInInspector] public int m_PlayerNumber;
        [HideInInspector] public string m_ColoredPlayerText;
        [HideInInspector] public GameObject m_InstancePlayer;
        [HideInInspector] public int m_WinsRounds;
        [HideInInspector] public PlayerHandler m_PlayerHandler;
        [HideInInspector] public PlayerAttributes m_PlayerAttributes;

        public void Setup()
        {
            m_PlayerHandler = m_InstancePlayer.GetComponent<PlayerHandler>();

            m_PlayerAttributes = m_PlayerHandler.m_Attributes;
            m_PlayerAttributes.PlayerNumber = m_PlayerNumber;
            m_PlayerAttributes.PlayerColor = m_PlayerColor;

            m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";

            MeshRenderer[] renderers = m_InstancePlayer.GetComponentsInChildren<MeshRenderer>();

            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material.color = m_PlayerColor;
            }
        }

        public void DisableControl()
        {
            m_PlayerHandler.DisablePlayer();
        }


        public void EnableControl()
        {
            m_PlayerHandler.EnablePlayer();
        }


        public void Reset()
        {
            m_PlayerHandler.ResetPlayer();

            m_InstancePlayer.transform.position = m_SpawnPoint.position;
            m_InstancePlayer.transform.rotation = m_SpawnPoint.rotation;

            m_InstancePlayer.SetActive(false);
            m_InstancePlayer.SetActive(true);
        }
    }
}