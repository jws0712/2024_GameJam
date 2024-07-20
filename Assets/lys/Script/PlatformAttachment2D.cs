using UnityEngine;

public class PlatformAttachment2D : MonoBehaviour
{
    private Vector3 originalScale;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Rigidbody2D>() != null)
        {
            originalScale = collision.transform.localScale;
            collision.transform.SetParent(transform);
            collision.transform.localScale = originalScale;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Rigidbody2D>() != null)
        {
            collision.transform.SetParent(null);
            collision.transform.localScale = originalScale;
        }
    }
}
