using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public bool IsStore = false;
    public GameObject StorePanel;
    private new SpriteRenderer renderer = null;
    [SerializeField] private Sprite Hsprite;
    [SerializeField] private Sprite Dsprite;
    // Start is called before the first frame update

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        StorePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && IsStore == true) 
        {
            StorePanel.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StorePanel.SetActive(false);
        }

        if(IsStore == true)
        {
            renderer.sprite = Hsprite;
        }
        else
        {
            renderer.sprite = Dsprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 태그수정
        {
            IsStore = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) // 태그수정
        {
            IsStore = false;
        }
    }
}
