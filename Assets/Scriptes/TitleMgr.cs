using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMgr : MonoBehaviour
{ 
    public GameObject obj;
    public GameObject SettingUI;
    public TMP_Dropdown dropdown;
    public Toggle fullscreenmode;
    private GameObject[] settinguis = new GameObject[2];
    private float ang = 0;

    private void Awake()
    {
        if(SettingUI !=null)
        {
            for(int i=0; i<settinguis.Length; i++) settinguis[i] = SettingUI.transform.GetChild(i).gameObject;
            SettingButton(false);
            dropdown.onValueChanged.AddListener(OnDropdownEvent);
            // 초기에 게임 해상도 고정
        }
    }

    private void OnDropdownEvent(int index)
    {
        Debug.Log(index);
       if(index == 0)
        {
            SetResolution(1920, 1080);
        }
       else if(index == 1)
        {
            SetResolution(11600, 900);
        }
       else if(index ==2)
        {
            SetResolution(1280, 720);
        }
    
        else if(index == 3)
        {
            SetResolution(854, 480);
        }
    }

    /* 해상도 설정하는 함수 */
    public void SetResolution(int w, int h)
    {
        int setWidth = w; // 사용자 설정 너비
        int setHeight = h; // 사용자 설정 높이

        //int deviceWidth = Screen.width; // 기기 너비 저장
        //int deviceHeight = Screen.height; // 기기 높이 저장
        bool modeset = fullscreenmode.isOn;
        if(modeset) Screen.SetResolution(setWidth, setHeight, FullScreenMode.Windowed);
        else if (!modeset) Screen.SetResolution(setWidth, setHeight, true);
        // Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution 함수 제대로 사용하기


        /*if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // 기기의 해상도 비가 더 큰 경우
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // 새로운 너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // 새로운 Rect 적용
        }
        else // 게임의 해상도 비가 더 큰 경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운 높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // 새로운 Rect 적용
        }*/


    }

    private void Update()
    {
        for (int i = 0; i < 100; i++)
        {
            obj.transform.rotation = Quaternion.Euler(new Vector3(5, ang, 0));
            ang += 1f;
        }
        for (int i = 0; i < 500; i++)
        {
            obj.transform.rotation = Quaternion.Euler(new Vector3(5, ang, 0)); 
            ang += 0.01f;
        }
        for (int i = 0; i < 100; i++)
        {
            obj.transform.rotation = Quaternion.Euler(new Vector3(5, ang, 0)); 
            ang -= 1f;
        }
        for (int i = 0; i < 500; i++)
        {
            obj.transform.rotation = Quaternion.Euler(new Vector3(5, ang, 0)); 
            ang -= 0.01f;
        }
    }
    public void startButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SettingButton(bool b)
    {
        if (SettingUI != null)
        {
            settinguis[0].SetActive(!b);
            settinguis[1].SetActive(b);
        }

    }
    public void ExitButton()
    {
        #if UNITY_EDITOR
             UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); // 어플리케이션 종료
        #endif
    }
}
