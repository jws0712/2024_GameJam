using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject Setting_panel; // 옵션 패널 
    public bool Setting_panel_on; // 옵션 패널이 열려있는가

    public Slider Master_Slider; // 마스터 슬라이더
    public Slider BGM_Slider; // 브금 슬라이더
    public Slider Effect_Slider; // 이펙트 슬라이더

    public Sprite mute_img; // 뮤트 이미지
    public Sprite unmute_img; // 음표 이미지

    public Image Master_img;// 마스터 볼륨 이미지
    public Image BGM_img;// 배경 볼룸 이미지
    public Image Effect_img;// 이펙트 볼륨 이미지

    //현재 뮤트가 되었는지 저장하는 변수
    private bool isMuteMasterVolum;
    private bool isMuteBGMVolum;
    private bool isMuteBGSVolum;

    //뮤트 시 이전 볼륨 저장
    public float masterVolum;
    public float bgsVolum;
    public float bgmVolum;

    void Start()
    {
        OnOffPannel(false);// 시작시 옵션 패널 꺼짐
 
    }
    private void OnOffPannel(bool active)
    {
        Setting_panel.SetActive(active);
        Setting_panel_on = active;
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && Setting_panel_on == true) // esc를 눌렀을 때 옵션 창 닫기 
        {
            OnOffPannel(false);
        }

        if (Master_Slider.value == 0.001f) 
        {
            Master_img.sprite = mute_img;
        }
        else
        {
            Master_img.sprite = unmute_img;
        }

        if (BGM_Slider.value == 0.001f) 
        {
            BGM_img.sprite = mute_img;
        }
        else
        {
            BGM_img.sprite = unmute_img;
        }

        if (Effect_Slider.value == 0.001f)  
        {
            Effect_img.sprite = mute_img;
        }
        else
        {
            Effect_img.sprite = unmute_img;
        }
    }
    public void StartGame()
    {
        // 로딩 씬으로 전환 (로딩 씬의 인덱스를 설정)
        SceneManager.LoadScene(1);
    }
    public void OptionpPanel()// 옵션창 열기 함수
    {
        OnOffPannel(true);

    }
    public void OptionpPanel_Close() // 옵션창 닫기
    {
        OnOffPannel(false);
    }
    public void ExitGame() // 게임 나가기 함수
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

    public void mute_MV() // 마스터 볼륨 음소거
    {
        if (isMuteMasterVolum)
        {
            Master_Slider.value = masterVolum;
        }
        else
        {
            masterVolum = Master_Slider.value;
            Master_Slider.value = 0.001f;
        }
        isMuteMasterVolum = !isMuteMasterVolum;
    }
    public void mute_EV() // 이펙트 볼륨 음소거
    {
        if (isMuteBGSVolum)
        {
            Effect_Slider.value = bgsVolum;
        }
        else
        {
            bgsVolum = Effect_Slider.value;
            Effect_Slider.value = 0.001f;
        }
        isMuteBGSVolum = !isMuteBGSVolum;
    }
    public void mute_BV() // 배경음악 음소거
    {
        if (isMuteBGMVolum)
        {
            BGM_Slider.value = bgmVolum;
        }
        else
        {
            bgmVolum = BGM_Slider.value;
            BGM_Slider.value = 0.001f;
        }
        isMuteBGMVolum = !isMuteBGMVolum;
    }
}
