using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_JWS : MonoBehaviour
{
    public static GameManager_JWS instance;


    private void Awake()
    {
        instance = this;
    }

    [Header("WorldStats")]
    [SerializeField] private float currentO2;

    [SerializeField] private float maxO2;
    [SerializeField] private float minO2;
    [Header("UI")]
    public Slider worldO2Slider;
    [SerializeField] private TextMeshProUGUI text;

    private Transform playerTransform = null;
    private float minPersent;

    private void Start()
    {
        minPersent = Mathf.Abs(minO2) / 100f;
    }

    private void Update()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;

        currentO2 = minPersent - Mathf.Abs(playerTransform.transform.position.y) / 100f;

        currentO2 = Mathf.Clamp(currentO2, 0f, 1f);

        worldO2Slider.value = currentO2;

        perSentUpdater();
    }

    private void perSentUpdater()
    {
        text.text = Mathf.FloorToInt(worldO2Slider.value * 100).ToString() + "%";
    }

}
