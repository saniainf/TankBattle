using System;
using UnityEngine;
using TankBattle;

namespace TankBattle
{
    [Serializable]
    public class TankManager
    {
        public Color m_PlayerColor;
        public Transform m_SpawnPoint;
        [HideInInspector] public int m_PlayerNumber;
        [HideInInspector] public string m_ColoredPlayerText;
        [HideInInspector] public GameObject m_Instance;
        [HideInInspector] public int m_Wins;
        [HideInInspector] public PlayerHandler m_PlayerHandler;
        [HideInInspector] public PlayerAttributes m_PlayerAttributes;

        public void Setup()
        {
            m_PlayerHandler = m_Instance.GetComponent<PlayerHandler>();
            m_PlayerAttributes = m_Instance.GetComponent<PlayerAttributes>();

            m_PlayerAttributes.PlayerNumber = m_PlayerNumber;
            m_PlayerAttributes.PlayerColor = m_PlayerColor;

            m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";

            MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();

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

            m_Instance.transform.position = m_SpawnPoint.position;
            m_Instance.transform.rotation = m_SpawnPoint.rotation;

            m_Instance.SetActive(false);
            m_Instance.SetActive(true);
        }
    }
}