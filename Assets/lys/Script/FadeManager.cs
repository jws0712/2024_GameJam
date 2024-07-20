using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<FadeManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("FadeManager");
                    instance = go.AddComponent<FadeManager>();
                }
            }
            return instance;
        }
    }
    private static FadeManager instance;

    public GameObject fadeOutUIImage;
    public float fadeSpeed = 1f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        instance = this;

        // Make sure the FadeManager object persists across scenes
        DontDestroyOnLoad(gameObject);

        // Ensure fadeOutUIImage is set up correctly
        if (fadeOutUIImage == null)
        {
            FindFadeOutUIImage();
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
        if (fadeOutUIImage == null)
        {
            FindFadeOutUIImage();
        }

        if (fadeOutUIImage != null)
        {
            fadeOutUIImage.SetActive(true);
            float alpha = 1f;
            while (alpha > 0f)
            {
                SetColorImage(ref alpha, true);
                yield return null;
            }
            fadeOutUIImage.SetActive(false);
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        if (fadeOutUIImage == null)
        {
            FindFadeOutUIImage();
        }

        if (fadeOutUIImage != null)
        {
            float alpha = 0f;
            fadeOutUIImage.SetActive(true);
            while (alpha < 1f)
            {
                SetColorImage(ref alpha, false);
                yield return null;
            }
        }
        else
        {
            Debug.LogWarning("FadeOutUIImage is not found.");
        }
    }

    private void SetColorImage(ref float alpha, bool isFadingIn)
    {
        Image image = fadeOutUIImage.GetComponent<Image>();
        if (image != null)
        {
            image.color = new Color(0f, 0f, 0f, alpha);
            alpha += Time.deltaTime * (1.0f / fadeSpeed) * (isFadingIn ? -1 : 1);
        }
        else
        {
            Debug.LogWarning("FadeOutUIImage does not have an Image component.");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FadeIn();
    }

    public void ChangeScene(int sceneIndex)
    {
        StartCoroutine(ChangeSceneCoroutine(sceneIndex));
    }

    private IEnumerator ChangeSceneCoroutine(int sceneIndex)
    {
        yield return FadeOutCoroutine();
        SceneManager.LoadScene(sceneIndex);
    }

    private void FindFadeOutUIImage()
    {
        // This assumes that the GameObject with the tag 'Fade' is in the current scene.
        fadeOutUIImage = GameObject.FindWithTag("Fade");
        if (fadeOutUIImage != null)
        {
            // Ensure that this GameObject persists across scenes
            DontDestroyOnLoad(fadeOutUIImage);
        }
    }
}
