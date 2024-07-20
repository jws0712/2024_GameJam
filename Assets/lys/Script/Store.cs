using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public bool IsStore = false;
    public GameObject StorePanel;
    // Start is called before the first frame update
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null) // 태그수정
        {
            IsStore = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision != null) // 태그수정
        {
            IsStore = false;
        }
    }
}
