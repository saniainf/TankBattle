using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider slider;

    private GameManager gameManager;
    private HandlerPlayerUI playerUI;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerUI != null)
        {
            slider.value = playerUI.PlayerHealth / 100;
        }
    }

    public void SetTanks(TankManager[] tankManager)
    {
        playerUI = tankManager[0].m_Instance.GetComponent<HandlerPlayerUI>();
    }
}
