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
            // �ʱ⿡ ���� �ػ� ����
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

    /* �ػ� �����ϴ� �Լ� */
    public void SetResolution(int w, int h)
    {
        int setWidth = w; // ����� ���� �ʺ�
        int setHeight = h; // ����� ���� ����

        //int deviceWidth = Screen.width; // ��� �ʺ� ����
        //int deviceHeight = Screen.height; // ��� ���� ����
        bool modeset = fullscreenmode.isOn;
        if(modeset) Screen.SetResolution(setWidth, setHeight, FullScreenMode.Windowed);
        else if (!modeset) Screen.SetResolution(setWidth, setHeight, true);
        // Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution �Լ� ����� ����ϱ�


        /*if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
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
            Application.Quit(); // ���ø����̼� ����
        #endif
    }
}
