using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    private Animator pickaxeAnimator = null;
    [SerializeField] private Collider2D miningDistance = null;
    [SerializeField] private Slider playerHp = null;
    [SerializeField] private Slider playerO2Slider = null;
    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;
    [SerializeField] private float maxPlayerO2;
    [SerializeField] private float currentPlayerO2;
    [SerializeField] private float o2UpgradeVelue;

    private Animator animator;


    private bool isMining;

    private void Awake()
    {
        pickaxeAnimator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHp = maxHp;
        currentPlayerO2 = maxPlayerO2;

        StartCoroutine(SliderUpdater());
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.E) && !isMining)
        {
            PlayerMining();
        }

        playerHp.value = currentHp / maxHp;
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

    private IEnumerator SliderUpdater()
    {

        while (playerO2Slider.value != 0)
        {
            if (GameManager.instance.worldO2Slider.value <= 0.2f)
            {
                currentPlayerO2 -= 4 * (o2UpgradeVelue / 100);
            }
            else if (GameManager.instance.worldO2Slider.value <= 0.4f)
            {
                currentPlayerO2 -= 3 * (o2UpgradeVelue / 100);

            }
            else if (GameManager.instance.worldO2Slider.value <= 0.6f)
            {
                currentPlayerO2 -= 2 * (o2UpgradeVelue / 100);

            }
            else if (GameManager.instance.worldO2Slider.value <= 0.8f)
            {
                currentPlayerO2 -= 1 * (o2UpgradeVelue / 100);

            }
            else if (GameManager.instance.worldO2Slider.value <= 1)
            {
                currentHp = maxHp;
                currentPlayerO2 = maxPlayerO2;

            }

            playerO2Slider.value = currentPlayerO2 / maxPlayerO2;

            yield return new WaitForSeconds(0.5f);
        }
        playerO2Slider.value = 0;

        if(playerO2Slider.value <= 0)
        {
            while(playerHp.value != 0)
            {
                currentHp -= 2f;

                playerHp.value = currentHp / maxHp;

                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.instance.GameOver();
        GameManager.instance.isPlayerDie = true;
        animator.SetTrigger("Die");
    }
}
