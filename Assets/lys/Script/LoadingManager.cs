using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public static LoadingManager Instance { get; private set; }

    public int targetSceneIndex; // 로드할 씬의 인덱스

    private void OnEnable()
    {
        StartCoroutine(LoadGameSceneAsync());
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 이 객체를 파괴하지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 이 객체를 파괴
        }
    }

    private void Start()
    {
        
        //StartCoroutine(LoadGameSceneAsync());
    }

    private IEnumerator LoadGameSceneAsync()
    {
        // 씬을 비동기적으로 로드
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(targetSceneIndex);

        //while (!asyncLoad.isDone)
        {
            // 로딩 진행 상태를 콘솔에 출력
            //float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // progress는 0에서 0.9까지 진행됨
            //Debug.Log("Loading Progress: " + (progress * 100f).ToString("F0") + "%");

            yield return new WaitForSeconds(1.2f);
            SceneManager.LoadScene(targetSceneIndex);
        }
    }
}
