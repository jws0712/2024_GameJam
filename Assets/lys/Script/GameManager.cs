using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMesh Pro 네임스페이스
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SpriteList
{
    public Sprite[] sprites;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //public Image[] itemImages; // UI에서 아이템 이미지를 표시할 Image 컴포넌트 배열
    //public List<SpriteList> enhancementSprites; // 각 아이템의 강화 레벨에 따른 이미지 배열을 관리하는 List
    //public TMP_Text[] enhancementLevelTexts; // 강화 레벨을 표시할 TMP_Text 컴포넌트 배열

    public pickAxeData[] pickAxeDatas;
    public int pickAxeIndex = 0;
    public float Coin;
    public pickAxeData currentPickAxeData;

    public GameObject Play_Panel;
    public GameObject Play_Sound_panel;
    public Slider Play_Master_Slider; // 마스터 슬라이더
    public Slider Play_BGM_Slider; // 브금 슬라이더
    public Slider Play_Effect_Slider; // 이펙트 슬라이더
    private ButtonManager ButtonManager;
    private bool Play_Sound_panel_ON = true;
    public bool isPlayerDie = false;

    

    private const string EnhancementLevelKeyPrefix = "EnhancementLevel_"; // PlayerPrefs 키 접두사

    [Header("WorldStats")]
    [SerializeField] private float currentO2;

    [SerializeField] private float maxO2;
    [SerializeField] private float minO2;
    [Header("UI")]
    public Slider worldO2Slider;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject gameClearPanel;
    [SerializeField] private GameObject gameOverPanel;
    public float o2UpgradeVelue;

    private Transform playerTransform = null;
    private float minPersent;

    private void Awake()
    {
        instance = this;

        
    }

    private void Start()
    {
        Coin = PlayerPrefs.GetInt("CoinCount", 0);
        pickAxeIndex = PlayerPrefs.GetInt("Index", 0);
        currentPickAxeData = pickAxeDatas[pickAxeIndex];
        o2UpgradeVelue = PlayerPrefs.GetInt("AIR", 100);
        minPersent = Mathf.Abs(minO2) / 100f;

        Play_Panel.SetActive(false);
        Play_Sound_panel.SetActive(false);


        // ButtonManager를 찾는 코드 추가
        ButtonManager = FindObjectOfType<ButtonManager>();
        if (ButtonManager == null)
        {
            return;
        }
        else
        {
            Play_Master_Slider.value = ButtonManager.masterVolum;
            Play_BGM_Slider.value = ButtonManager.bgmVolum;
            Play_Effect_Slider.value = ButtonManager.bgsVolum;
        }

        FadeManager.Instance.FadeIn();
        //// 각 아이템의 저장된 강화 레벨을 불러와서 UI를 업데이트
        //for (int i = 0; i < itemImages.Length; i++)
        //{
        //    int enhancementLevel = PlayerPrefs.GetInt(EnhancementLevelKeyPrefix + i, 0);
        //    UpdateItemDisplay(i, enhancementLevel);
        //}
    }

    private void Update()
    {
        currentPickAxeData = pickAxeDatas[pickAxeIndex];

        if(isPlayerDie == true)
        {
            return;
        }

        coinText.text = "X " + Coin.ToString();

        if (Input.GetKeyDown(KeyCode.Escape) && Play_Sound_panel_ON == false)
        {
            ToggleSettingsPanel();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && Play_Sound_panel_ON == true)
        {
            Play_Sound_panel.SetActive(false);
            Play_Sound_panel_ON = false;
        }

        Logic();
        perSentUpdater();
    }

    public void ToggleSettingsPanel()
    {
        if (Play_Panel != null)
        {
            bool isActive = Play_Panel.activeSelf;  // 현재 활성화 상태를 확인합니다.
            Play_Panel.SetActive(!isActive);        // 활성화 상태를 토글합니다.
        }
    }

    public void open_PlaySound()
    {
        if (ButtonManager != null)
        {
            Play_Sound_panel.SetActive(true);
            Play_Sound_panel_ON = true;
        }
        else
        {
            Debug.LogWarning("ButtonManager가 null입니다. 설정 패널을 열 수 없습니다.");
        }
    }

    public void GO_Title()
    {
        // 로딩 씬으로 전환 (로딩 씬의 인덱스를 설정)
        FadeManager.Instance.ChangeScene(0);
    }

    public void UpGradePickAxe()
    {
        if(Coin > 5 && pickAxeIndex < 9)
        {
            Coin -= 5;
            pickAxeIndex++;
            PlayerPrefs.SetInt("Index", pickAxeIndex);
            PlayerPrefs.SetInt("CoinCount", (int)Coin);
        }
    }

    public void UpGradeAir()
    {
        if (Coin > 10 && o2UpgradeVelue > 20)
        {
            Coin -= 10;
            o2UpgradeVelue -= 10;
            PlayerPrefs.SetInt("AIR", (int)o2UpgradeVelue);
            PlayerPrefs.SetInt("CoinCount", (int)Coin);
            
        }
    }


    //// 아이템 강화 메서드
    //public void EnhanceItem(int itemIndex)
    //{
    //    // 지정된 아이템의 현재 강화 레벨을 불러옴
    //    int enhancementLevel = PlayerPrefs.GetInt(EnhancementLevelKeyPrefix + itemIndex, 0);

    //    // 강화 레벨이 스프라이트 배열의 길이보다 작으면 강화 진행
    //    if (enhancementLevel < enhancementSprites[itemIndex].sprites.Length - 1)
    //    {
    //        enhancementLevel++;
    //        PlayerPrefs.SetInt(EnhancementLevelKeyPrefix + itemIndex, enhancementLevel);
    //        PlayerPrefs.Save(); // 데이터 저장
    //        UpdateItemDisplay(itemIndex, enhancementLevel);
    //    }
    //    else
    //    {
    //        Debug.Log("아이템 " + itemIndex + "의 최대 강화 레벨에 도달했습니다.");
    //    }
    //}

    //// 아이템 이미지와 텍스트 업데이트 메서드
    //private void UpdateItemDisplay(int itemIndex, int enhancementLevel)
    //{
    //    if (itemIndex < itemImages.Length && enhancementLevel < enhancementSprites[itemIndex].sprites.Length)
    //    {
    //        itemImages[itemIndex].sprite = enhancementSprites[itemIndex].sprites[enhancementLevel];
    //        enhancementLevelTexts[itemIndex].text = "LEVEL: " + (enhancementLevel + 1);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("아이템 " + itemIndex + "의 잘못된 강화 레벨입니다.");
    //    }
    //}

    //public void ResetPlayerPrefs() // 강화 단계 초기화
    //{
    //    // 모든 PlayerPrefs 데이터 삭제
    //    PlayerPrefs.DeleteAll();

    //    // 모든 아이템의 강화 레벨을 0으로 설정
    //    for (int i = 0; i < itemImages.Length; i++)
    //    {
    //        PlayerPrefs.SetInt(EnhancementLevelKeyPrefix + i, 0);
    //    }

    //    PlayerPrefs.Save(); // 변경 사항 저장
    //    Debug.Log("PlayerPrefs가 초기화되었습니다. 모든 아이템의 강화 단계가 1부터 시작됩니다.");
    //}

    private void Logic()
    {
        playerTransform = GameObject.FindWithTag("Player").transform;

        currentO2 = minPersent - Mathf.Abs(playerTransform.transform.position.y) / 100f;

        currentO2 = Mathf.Clamp(currentO2, 0f, 1f);

        worldO2Slider.value = currentO2;
    }

    private void perSentUpdater()
    {
        text.text = Mathf.FloorToInt(worldO2Slider.value * 100).ToString() + "%";
    }

    public void QuitButton()
    {
        SceneManager.LoadScene(0);
    }

    public void ReStartButton()
    {
        SceneManager.LoadScene(2);
    }

    public void GameClear()
    {
        gameClearPanel.SetActive(true);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
