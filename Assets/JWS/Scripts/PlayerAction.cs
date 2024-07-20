using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private Animator pickaxeAnimator = null;
    [SerializeField] private Collider2D miningDistance = null;

    private bool isMining;

    private void Awake()
    {
        pickaxeAnimator = transform.GetChild(0).gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.E) && !isMining)
        {
            PlayerMining();
        }
    }

    private void PlayerMining()
    {
        StartCoroutine(Mining());
    }

    private IEnumerator Mining()
    {
        isMining = true;
        pickaxeAnimator.SetTrigger("Mining");
        miningDistance.enabled = true;
        yield return new WaitForSeconds(0.1f);
        miningDistance.enabled = false;
        yield return new WaitForSeconds(0.4f);
        isMining = false;
    }
}
