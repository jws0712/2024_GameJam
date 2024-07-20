using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("PlayerSetting")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private GameObject charactorHolder = null;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPos;
    [SerializeField] private Transform tp_pos = null;
    [SerializeField] private GameObject pickaxe = null;

    private Rigidbody2D rb = null;
    private Animator animator = null;

    private float horizontal = default;

    private Vector2 dir = Vector2.zero;
    private Vector3 originalSize = Vector3.zero;

    public bool faceRight = default;
    private bool isJump = default;
    public bool isTp = default;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        faceRight = true;
        isTp = false;
    }

    private void Update()
    {

        if (isTp == true && GroundCheck() == false)
        {
            isTp = false;
            pickaxe.SetActive(!isTp);
        }

        if (isTp == true || GameManager.instance.isPlayerDie == true)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.Q) && GroundCheck() && !isTp)
        {
            isTp = true;
            pickaxe.SetActive(!isTp);
            animator.SetTrigger("Tp");
        }

        PlayerInput();
        PlayerFlip();
        PlayerJump();
        SetPlayerAnimation();
    }

    private void FixedUpdate()
    {
        if(isTp == true || GameManager.instance.isPlayerDie == true)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        PlayerMove();
    }

    public void Tp()
    {
        transform.position = tp_pos.position;
    }

    private void PlayerJump()
    {
        if (GroundCheck() && Input.GetKeyDown(KeyCode.Space))
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
        if(rb.velocity.y < 0f && !GroundCheck())
        {
            rb.gravityScale = 2f;
        }

        if (GroundCheck())
        {
            rb.gravityScale = 1f;
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

        if (!GroundCheck())
        {
            animator.SetBool("isJump", true);
        }
        else
        {
            animator.SetBool("isJump", false);
        }

        animator.SetFloat("Vertical", rb.velocity.y);
    }

    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, groundLayer);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ore"))
        {
            collision.GetComponent<Ore>().TakeDamage(transform.GetChild(0).GetComponent<pickAxe>().damage);
        }
    }
}
