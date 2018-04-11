using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public GameObject BonusPrefab;

    private GameObject bonus;

    private float timer = 3.0f;

    // Use this for initialization
    void Start()
    {
        bonus = Instantiate(BonusPrefab, new Vector3(transform.position.x, BonusPrefab.transform.position.y, transform.position.z), BonusPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        if (!bonus.activeSelf)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0)
        {
            bonus.SetActive(true);
            timer = 3.0f;
        }
    }
}
