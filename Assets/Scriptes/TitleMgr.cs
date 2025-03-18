using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMgr : MonoBehaviour
{
    private static TitleMgr instance;

    public GameObject obj;
    public GameObject SettingUI;
    public TMP_Dropdown dropdown;
    public Toggle fullscreenmode;
    private GameObject[] settinguis = new GameObject[2];
    private float ang = 0;
    private bool firstgamestart = false;
    private bool isingame = false;

    private void Awake()
    {
            if (null == instance)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
                if (SettingUI != null)
                {
                    for (int i = 0; i < settinguis.Length; i++) settinguis[i] = SettingUI.transform.GetChild(i).gameObject;
                   SettingButton(false);
                   dropdown.onValueChanged.AddListener(OnDropdownEvent);
                    SceneManager.sceneLoaded += LoadedsceneEvent;
                }
            
            }
            else
            {
                Destroy(gameObject);
            }
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().name.Equals("Title"))
        {
            if(firstgamestart) gameObject.SetActive(true);
            unitSM();
        }
        else
        {
            gameObject.SetActive(false);
           if(!firstgamestart)  firstgamestart = true;
        }
    }

    private void unitSM()
    {
        if (obj == null) obj = GameObject.Find("Main Camera").gameObject;
        isingame = false;
        SettingButton(false);
    }

    public IEnumerator ingameSettingSM()
    {
        isingame = true;
        settinguis[0].SetActive(false);
        settinguis[1].SetActive(false);
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //SettingButton(true);
                SceneManager.LoadScene("Title");
                yield break;
            }
            
            yield return null;
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
            SetResolution(1600, 900);
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

    public void savefullscreenmode()
    {
        SetResolution(Screen.width,Screen.height);
    }

    /* 해상도 설정하는 함수 */
    public void SetResolution(int w, int h)
    {
        int setWidth = w; // 사용자 설정 너비
        int setHeight = h; // 사용자 설정 높이

        //int deviceWidth = Screen.width; // 기기 너비 저장
        //int deviceHeight = Screen.height; // 기기 높이 저장
        bool modeset = fullscreenmode.isOn;
        if(!modeset) Screen.SetResolution(setWidth, setHeight, FullScreenMode.Windowed);
        else if (modeset) Screen.SetResolution(setWidth, setHeight, true);
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
        if (obj != null)
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
    }
    public void startButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SettingButton(bool b)
    {
        if (SettingUI != null)
        {
            if (isingame)
            {
                settinguis[0].SetActive(false);
                settinguis[1].SetActive(b);
                if (GameMgr.Instance.UI != null)GameMgr.Instance.UI.GetComponent<UIMgr>().setactivemecUI(!b);
            }
            else
            {
                settinguis[0].SetActive(!b);
                settinguis[1].SetActive(b);
            }
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

    public static TitleMgr Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
}
