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
    public TMP_Text[] upgradeUI = new TMP_Text[5];
    public bool isupgardemode = false;
    public bool isrefundmode = false;
    private int temprefundprice = 0;
    private int tempturretupgrade = 0;

    private void LateUpdate()
    {
        if (stats != null)
        {
            if(install != null) stats.transform.position = install.transform.position + new Vector3(x_preset,y_preset,0);
            stats.transform.forward = Camera.main.transform.forward;
            if (GameMgr.Instance != null && upgradeUI.Length >= 5)
            {
                if (upgradeUI[4] != null)
                {
                    if (isrefundmode) upgradeUI[4].text = "Coin : " + GameMgr.Instance.getcoins() + " > " + (GameMgr.Instance.getcoins() + temprefundprice);
                    else if (isupgardemode)
                    {
                        if ((GameMgr.Instance.getcoins() - tempturretupgrade) > 0) upgradeUI[4].text = "Coin : " + GameMgr.Instance.getcoins() + " > " + (GameMgr.Instance.getcoins() - tempturretupgrade);
                        else upgradeUI[4].text = "Coin : 0";
                    }
                    else upgradeUI[4].text = "Coin : " + GameMgr.Instance.getcoins();
                }
            }
        }
    }
    public void apperupgradecost()
    {
        upgradeUI[0].GetComponent<RectTransform>().localPosition = new Vector3(0, 90f, 0);
        upgradeUI[1].gameObject.SetActive(false);
        upgradeUI[2].GetComponent<RectTransform>().localPosition = new Vector3(0, 25f, 0);
        upgradeUI[3].gameObject.SetActive(false);
    }

    public void turretgrademode()
    {
        MecMgr mecMgr = install.GetComponent<MecMgr>();
        MecMgr.turretUpgradeInfo info = mecMgr.getturretinfo();
        if (isupgardemode)
        {
            upgradeUI[0].GetComponent<RectTransform>().localPosition = new Vector3(0, 90f, 0);
            upgradeUI[1].GetComponent<RectTransform>().localPosition = new Vector3(0, 25f, 0);
            upgradeUI[1].gameObject.SetActive(true);
            upgradeUI[2].gameObject.SetActive(false);
            upgradeUI[3].gameObject.SetActive(false);
        } else if(isrefundmode)
        {
            upgradeUI[2].GetComponent<RectTransform>().localPosition = new Vector3(0, 90f, 0);
            upgradeUI[3].GetComponent<RectTransform>().localPosition = new Vector3(0, 25f, 0);
            upgradeUI[3].gameObject.SetActive(true);
            upgradeUI[0].gameObject.SetActive(false);
            upgradeUI[1].gameObject.SetActive(false);
            if (isrefundmode) upgradeUI[2].text = "Confirm To Refund - X";
            else upgradeUI[2].text = "Refund(50%) - X";
            temprefundprice = ((info.t_totalcost * 50) / 100);
            upgradeUI[3].text = "Will Get [" + temprefundprice + "] coins";
        }
            if (mecMgr.upgradelevel >= mecMgr.maxupgradelevel)
            {
                upgradeUI[0].text = "Turret Upgarde [MAX]";
                upgradeUI[1].text = "";
            }
            else
            {
                if (isupgardemode) upgradeUI[0].text = "Confirm To Upgrade - F";
                else upgradeUI[0].text = "Turret Upgarde [" + mecMgr.upgradelevel + "] - F";
                tempturretupgrade = info.t_upgradecost[mecMgr.upgradelevel];
                upgradeUI[1].text = "Upgrade Cost [" + tempturretupgrade + "] coins";
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
