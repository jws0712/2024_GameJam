using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickAxe : MonoBehaviour
{
    private new SpriteRenderer renderer;
    public float damage;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (GameManager.instance.currentPickAxeData == null)
        {
            Debug.LogError("GameManager instance or currentPickAxeData is null");
            return;
        }

        renderer.sprite = GameManager.instance.currentPickAxeData.PickAxeSprite;
        damage = GameManager.instance.currentPickAxeData.PickAxeDamage;
    }
}
