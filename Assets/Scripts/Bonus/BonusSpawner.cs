using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public GameObject bonuses;

    private GameObject m_BonusShield;

    private float timer = 3.0f;

    // Use this for initialization
    void Start()
    {
        m_BonusShield = Instantiate(bonuses, bonuses.transform.position, bonuses.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_BonusShield.activeSelf)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0)
        {
            m_BonusShield.SetActive(true);
            timer = 3.0f;
        }
    }
}
