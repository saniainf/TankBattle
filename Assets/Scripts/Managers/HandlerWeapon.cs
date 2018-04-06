using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerWeapon : MonoBehaviour
{
    public int m_PlayerNumber = 1;

    private string m_FireButton;

    private Weapon m_Weapon;

    private void OnEnable()
    {
        AddWeapon(new WeaponStd());
    }

    // Use this for initialization
    void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;
    }

    public void AddWeapon(Weapon weapon)
    {
        m_Weapon = weapon;
        m_Weapon.Setup(this);
    }

    // Update is called once per frame
    void Update()
    {
        m_Weapon.Update();

        if (Input.GetButtonDown(m_FireButton))
        {
            m_Weapon.OnButtonDown();   
        }
        else if (Input.GetButton(m_FireButton))
        {
            m_Weapon.OnButtonPress();
        }
        else if (Input.GetButtonUp(m_FireButton))
        {
            m_Weapon.OnButtonUp();
        }

    }
}
