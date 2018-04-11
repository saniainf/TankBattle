﻿using System;
using UnityEngine;
using TankBattle;

[Serializable]
public class TankManager
{
    public Color m_PlayerColor;
    public Transform m_SpawnPoint;
    [HideInInspector] public int m_PlayerNumber;
    [HideInInspector] public string m_ColoredPlayerText;
    [HideInInspector] public GameObject m_Instance;
    [HideInInspector] public int m_Wins;


    //private TankMovement m_Movement;
    //private StandartTurret m_Shooting;
    //private GameObject m_CanvasGameObject;
    private PlayerHandler m_PlayerHandler;
    private PlayerAttributes m_PlayerAttributes;

    public void Setup()
    {
        //m_Movement = m_Instance.GetComponent<TankMovement>();
        //m_Shooting = m_Instance.GetComponentInChildren<StandartTurret>();
        //m_CanvasGameObject = m_Instance.GetComponentInChildren<Canvas>().gameObject;

        //m_Movement.m_PlayerNumber = m_PlayerNumber;
        //m_Shooting.m_PlayerNumber = m_PlayerNumber;
        m_PlayerHandler = m_Instance.GetComponent<PlayerHandler>();
        m_PlayerAttributes = m_Instance.GetComponent<PlayerAttributes>();

        m_PlayerAttributes.PlayerNumber = m_PlayerNumber;

        m_ColoredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColor) + ">PLAYER " + m_PlayerNumber + "</color>";

        MeshRenderer[] renderers = m_Instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = m_PlayerColor;
        }
    }


    public void DisableControl()
    {
        //m_Movement.enabled = false;
        //m_Shooting.enabled = false;

        //m_CanvasGameObject.SetActive(false);
        m_PlayerHandler.DisablePlayer();
    }


    public void EnableControl()
    {
        //m_Movement.enabled = true;
        //m_Shooting.enabled = true;

        //m_CanvasGameObject.SetActive(true);
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
