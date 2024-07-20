using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance
    {
        get
        {
            return instance;
        }
    }
    private static FadeManager instance;
    public Image fadeOutUIImage;
    public float fadeSpeed = 1f;

    void Start()
    {
        if (instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {

    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        fadeOutUIImage.enabled = true;
        float alpha = 1;
        while (alpha > 0)
        {
            SetColorImage(ref alpha, true);
            yield return null;
        }
        fadeOutUIImage.enabled = false;
    }

    private IEnumerator FadeOutCoroutine()
    {
        float alpha = 0;
        fadeOutUIImage.enabled = true;
        while (alpha < 1)
        {
            SetColorImage(ref alpha, false);
            yield return null;
        }
    }

    private void SetColorImage(ref float alpha, bool isFadingIn)
    {
        fadeOutUIImage.color = new Color(fadeOutUIImage.color.r, fadeOutUIImage.color.g, fadeOutUIImage.color.b, alpha);
        alpha += Time.deltaTime * (1.0f / fadeSpeed) * (isFadingIn ? -1 : 1);
    }
}