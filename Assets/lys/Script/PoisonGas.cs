using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PoisonGas : MonoBehaviour
{
    public bool IsGas = false;
    public int HP = 50; // 테스트용
    
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
        if (collision != null) // 태크 수정 해주세여
        {
            IsGas = true;// 들어갔을때 바로 중독
            if (IsGas == true)
            {
                InvokeRepeating("Test_TakeDamage", 0.1f, 0.5f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) // 태그수정 해주세여 
    {
        if (collision != null)
        {
            IsGas = false; // 나왔을 때 바로 해제 
            CancelInvoke("Test_TakeDamage");
        }
    }

    public void Test_TakeDamage() // TakeDamage 자리 입니다
    {
        HP -= 5;
    }
}
