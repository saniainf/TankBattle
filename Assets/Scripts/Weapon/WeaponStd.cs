using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStd : Weapon
{
    private float m_MinLaunchForce = 15f;
    private float m_MaxLaunchForce = 30f;
    private float m_MaxChargeTime = 0.75f;

    private HandlerWeapon m_HandlerWeapon;
    private float m_ChargeSpeed;
    private Slider m_AimSlider;

    public override void Setup(HandlerWeapon handlerWeapon)
    {
        m_HandlerWeapon = handlerWeapon;
        m_AimSlider = m_HandlerWeapon.gameObject.GetComponent<Slider>();
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }

    public override void Update()
    {
        m_AimSlider.value = m_MinLaunchForce - m_MinLaunchForce;
    }

    public override void OnButtonDown()
    {

    }

    public override void OnButtonPress()
    {

    }

    public override void OnButtonUp()
    {

    }
}
