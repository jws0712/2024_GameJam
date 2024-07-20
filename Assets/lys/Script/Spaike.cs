using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spaike : MonoBehaviour
{
    public bool isInvincible = false;
    public float invincibilityDuration = 2f; // 무적 시간
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerAction>().TakeDamage(20f);
            
        }
    }
}
