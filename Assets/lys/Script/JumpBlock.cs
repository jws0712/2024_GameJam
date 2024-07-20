using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBlock : MonoBehaviour
{
    public Rigidbody2D rigid;

    Vector2 JumpBlock_Power = new Vector2 (0, 10);
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null) 
        {
            rigid.AddForce(JumpBlock_Power, ForceMode2D.Impulse);
        }
    }
}
