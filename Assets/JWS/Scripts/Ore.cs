using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    [Header("OreSetting")]
    [SerializeField] private OreData oreData;
    [SerializeField] private float oreHp;
    [SerializeField] private float coinCount;

    private void Start()
    {
        oreHp = oreData.OreHp;
        coinCount = oreData.CoinCount;
    }

    public void TakeDamage(float damage)
    {
        oreHp -= damage;

        if(oreHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);   
    }
}
