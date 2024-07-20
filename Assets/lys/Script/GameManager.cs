using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMesh Pro 네임스페이스
using System.Collections.Generic;

[System.Serializable]
public class SpriteList
{
    public Sprite[] sprites;
}

public class GameManager : MonoBehaviour
{
    public Image[] itemImages; // UI에서 아이템 이미지를 표시할 Image 컴포넌트 배열
    public List<SpriteList> enhancementSprites; // 각 아이템의 강화 레벨에 따른 이미지 배열을 관리하는 List
    public TMP_Text[] enhancementLevelTexts; // 강화 레벨을 표시할 TMP_Text 컴포넌트 배열

    private const string EnhancementLevelKeyPrefix = "EnhancementLevel_"; // PlayerPrefs 키 접두사

    private void Start()
    {
        FadeManager.Instance.FadeIn();
        // 각 아이템의 저장된 강화 레벨을 불러와서 UI를 업데이트
        for (int i = 0; i < itemImages.Length; i++)
        {
            int enhancementLevel = PlayerPrefs.GetInt(EnhancementLevelKeyPrefix + i, 0);
            UpdateItemDisplay(i, enhancementLevel);
        }
    }

    // 아이템 강화 메서드
    public void EnhanceItem(int itemIndex)
    {
        // 지정된 아이템의 현재 강화 레벨을 불러옴
        int enhancementLevel = PlayerPrefs.GetInt(EnhancementLevelKeyPrefix + itemIndex, 0);

        // 강화 레벨이 스프라이트 배열의 길이보다 작으면 강화 진행
        if (enhancementLevel < enhancementSprites[itemIndex].sprites.Length - 1)
        {
            enhancementLevel++;
            PlayerPrefs.SetInt(EnhancementLevelKeyPrefix + itemIndex, enhancementLevel);
            PlayerPrefs.Save(); // 데이터 저장
            UpdateItemDisplay(itemIndex, enhancementLevel);
        }
        else
        {
            Debug.Log("아이템 " + itemIndex + "의 최대 강화 레벨에 도달했습니다.");
        }
    }

    // 아이템 이미지와 텍스트 업데이트 메서드
    private void UpdateItemDisplay(int itemIndex, int enhancementLevel)
    {
        if (itemIndex < itemImages.Length && enhancementLevel < enhancementSprites[itemIndex].sprites.Length)
        {
            itemImages[itemIndex].sprite = enhancementSprites[itemIndex].sprites[enhancementLevel];
            enhancementLevelTexts[itemIndex].text = "LEVEL: " + (enhancementLevel + 1);
        }
        else
        {
            Debug.LogWarning("아이템 " + itemIndex + "의 잘못된 강화 레벨입니다.");
        }
    }

    public void ResetPlayerPrefs() // 강화 단계 초기화
    {
        // 모든 PlayerPrefs 데이터 삭제
        PlayerPrefs.DeleteAll();

        // 모든 아이템의 강화 레벨을 0으로 설정
        for (int i = 0; i < itemImages.Length; i++)
        {
            PlayerPrefs.SetInt(EnhancementLevelKeyPrefix + i, 0);
        }

        PlayerPrefs.Save(); // 변경 사항 저장
        Debug.Log("PlayerPrefs가 초기화되었습니다. 모든 아이템의 강화 단계가 1부터 시작됩니다.");
    }
}
