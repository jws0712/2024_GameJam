using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System.Text;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerSetting")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private GameObject charactorHolder = null;

    private Rigidbody2D rb = null;
    private Animator animator = null;

    private float horizontal = default;

    private Vector2 dir = Vector2.zero;
    private Vector3 originalSize = Vector3.zero;

    public bool faceRight = default;
    public bool isGround = default;
    private bool isJump = default;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        faceRight = true;
    }

    private void Update()
    {
        PlayerInput();
        PlayerFlip();
        PlayerJump();
        SetPlayerAnimation();
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

            StartCoroutine(JumpSqueeze(0.5f, 1.2f, 0.1f));

            isJump = true;
        }
        if (isJump && Input.GetKeyUp(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            isJump = false;
        }
        if(rb.velocity.y < 0f && !isGround)
        {
            rb.gravityScale = 2f;
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
        if ((horizontal > 0 && !faceRight) || (horizontal < 0 && faceRight))
        {
            faceRight = !faceRight;
            transform.rotation = Quaternion.Euler(0, faceRight ? 0 : 180, 0);
        }
    }

    private IEnumerator JumpSqueeze(float xSquezze, float ySquezze, float seconds)
    {
        originalSize = Vector3.one;

        Vector3 newSize = new Vector3(xSquezze, ySquezze, originalSize.z);
        float t = 0;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            charactorHolder.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            yield return null;
        }
        t = 0;
        while((t <= 1.0))
        {
            t += Time.deltaTime / seconds;
            charactorHolder.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            yield return null;
        }
    }

    private void SetPlayerAnimation()
    {
        if(horizontal == 0)
        {
            animator.SetBool("isMove", false);
        }
        else
        {
            animator.SetBool("isMove", true);
        }

        if (!isGround)
        {
            animator.SetBool("isJump", true);
        }
        else
        {
            animator.SetBool("isJump", false);
        }

        animator.SetFloat("Vertical", rb.velocity.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isGround = true;
            rb.gravityScale = 1f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGround = false;
    }
}
