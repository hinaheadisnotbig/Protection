using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class InfoMgr : MonoBehaviour
{
    public GameObject stats;
    public GameObject install;
    public float x_preset = 2.4f;
    public float y_preset = 1.8f;

    private void LateUpdate()
    {
        if (stats != null)
        {
            if(install != null) stats.transform.position = install.transform.position + new Vector3(x_preset,y_preset,0);
            stats.transform.forward = Camera.main.transform.forward;
        }
    }

}
