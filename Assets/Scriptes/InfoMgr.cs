using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class InfoMgr : MonoBehaviour
{
    public GameObject stats;
    public GameObject install;
    public float x_preset = 2.4f;
    public float y_preset = 1.8f;
    public TurretCalSM turretcalsm;
    private TMP_Text[] upgradeUI = new TMP_Text[2];
    public bool isupgardemode = false;


    private void Awake()
    {
        for (int i = 0; i < upgradeUI.Length; i++) upgradeUI[i] = gameObject.transform.GetChild(0).GetChild(i).GetComponent<TMP_Text>();
    }
    private void LateUpdate()
    {
        if (stats != null)
        {
            if(install != null) stats.transform.position = install.transform.position + new Vector3(x_preset,y_preset,0);
            stats.transform.forward = Camera.main.transform.forward;
        }
    }

    public void turretgrademode()
    {
        MecMgr mecMgr = install.GetComponent<MecMgr>();
        MecMgr.turretUpgradeInfo info = mecMgr.getturretinfo();
        if(mecMgr.upgradelevel >= mecMgr.maxupgradelevel)
        {
            upgradeUI[0].text = "Turret Upgarde [MAX]";
            upgradeUI[1].text = "";
        } else {
            if(isupgardemode) upgradeUI[0].text = "Turret Upgrade Confirm - F";
            else upgradeUI[0].text = "Turret Upgarde [" + mecMgr.upgradelevel + "] - F";
            upgradeUI[1].text = "Upgrade Cost [" + info.t_upgradecost[mecMgr.upgradelevel] + "] coins";
        }
    }

    public void setinstall(GameObject obj)
    {
        install = obj;
        if (turretcalsm != null) turretcalsm.GetComponent<TurretCalSM>().updatturretinfo(install.GetComponent<MecMgr>());
    }
    public void setinstallanother(GameObject obj)
    {
        install = obj;
        if (turretcalsm != null) turretcalsm.GetComponent<TurretCalSM>().updatturretinfo(install.GetComponent<InstallMgr>().mecObj.GetComponent<MecMgr>());
    }

}
