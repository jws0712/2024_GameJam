using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spaike : MonoBehaviour
{
    public bool isInvincible = false;
    public float invincibilityDuration = 2f; // 무적 시간
    public int HP = 50; // 변경
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
        if(collision.CompareTag("Player") && isInvincible == false)
        {
            test_TakeDamage();
            isInvincible = true;
        }
        if (isInvincible == true)
        {
            Invoke("isInvincible_off", 2f);
        }
    }

    public void isInvincible_off()
    {
        isInvincible = false ;
    }

    public void test_TakeDamage() // 변경
    {
        HP -= 20;
    }
}
