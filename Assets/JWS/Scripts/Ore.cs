using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    [Header("OreSetting")]
    [SerializeField] private OreData oreData;
    [SerializeField] private float oreHp;
    [SerializeField] private float coinCount;
    [SerializeField] private Sprite oreSprite;
    private new SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        oreHp = oreData.OreHp;
        coinCount = oreData.CoinCount;
        oreSprite = oreData.OreSprite;

        renderer.sprite = oreSprite;
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
        GameManager.instance.Coin += coinCount;
        PlayerPrefs.SetInt("CoinCount", (int)GameManager.instance.Coin);
        Destroy(gameObject);   
    }
}
