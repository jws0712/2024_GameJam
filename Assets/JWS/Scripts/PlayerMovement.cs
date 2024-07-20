using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Text;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerSetting")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 10f;

    private Rigidbody2D rb = null;
    private Animator animator = null;
    private float horizontal = default;
    private Vector2 dir = Vector2.zero;
    private bool faceLeft = default;
    public bool isGround = default;
    private bool isJump = default;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        PlayerInput();
        PlayerFlip();
        PlayerJump();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    private void PlayerJump()
    {
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJump = true;
        }
        if (isJump && Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            isJump = false;
        }
    }

    private void PlayerMove()
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    private void PlayerInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }

    private void PlayerFlip()
    {
        if ((horizontal > 0 && !faceLeft) || (horizontal < 0 && faceLeft))
        {
            faceLeft = !faceLeft;
            transform.rotation = Quaternion.Euler(0, faceLeft ? 180 : 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = false;
        }
    }
}
