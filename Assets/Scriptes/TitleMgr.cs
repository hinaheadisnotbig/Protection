using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMgr : MonoBehaviour
{ 
    public GameObject obj;
    private float ang = 0;

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
}
