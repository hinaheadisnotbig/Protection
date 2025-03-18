using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TurretCalSM : MonoBehaviour
{
    public TMP_Text[] infoUItext = new TMP_Text[6];
    public bool isupgrademode = false;
    public void updatturretinfo(MecMgr mec)
    {
        if (mec == null) return;
        string tempstring;
        if (!isupgrademode)
        {
            if (mec.stats != null) tempstring = mec.stats.transform.GetChild(0).GetComponent<TMP_Text>().text;
            else tempstring = "null";
            infoUItext[0].text = "" + tempstring + "";
            infoUItext[1].text = "Damage - " + mec.basedamage;
            infoUItext[2].text = "Attack Speed - " + mec.attackspeed + "(s)";
            infoUItext[3].text = "Range - " + mec.range;
            infoUItext[4].text = "Bullet Speed - " + mec.bulletspeed;
            infoUItext[5].text = "DPS - " + Mathf.Round((mec.basedamage * (1 / mec.attackspeed)) * 10) * 0.1f;
        }
        else
        {
            bool isMaxlvl;
            int nextlvl = mec.upgradelevel + 1;
            if (mec.upgradelevel >= mec.maxupgradelevel) isMaxlvl = true;
            else isMaxlvl = false;
            MecMgr.turretUpgradeInfo tui = mec.getturretinfo();
            //for(int i = 0; i< infoUItext.Length; i++) infoUItext[i].color = Color.green;
            if (mec.stats != null) tempstring = mec.stats.transform.GetChild(0).GetComponent<TMP_Text>().text;
            else tempstring = "null";
            if (!isMaxlvl) {
                infoUItext[0].text = "" + tempstring + " > " + nextlvl + "";
                infoUItext[1].text = "Damage - " + mec.basedamage + " > " + tui.t_basedamage[nextlvl];
                infoUItext[2].text = "Attack Speed - " + mec.attackspeed + "(s)" + " > " + tui.t_attackspeed[nextlvl] + "(s)";
                infoUItext[3].text = "Range - " + mec.range + " > " + tui.t_range[nextlvl];
                infoUItext[4].text = "Bullet Speed - " + mec.bulletspeed + " > " + tui.t_bulletspeed[nextlvl]; ;
                infoUItext[5].text = "DPS - " + Mathf.Round((mec.basedamage * (1 / mec.attackspeed)) * 10) * 0.1f + " > " + Mathf.Round((tui.t_basedamage[nextlvl] * (1 / tui.t_attackspeed[nextlvl])) * 10) * 0.1f;
            }
            else {
                infoUItext[0].text = "" + tempstring + "";
                infoUItext[1].text = "Damage - " + mec.basedamage;
                infoUItext[2].text = "Attack Speed - " + mec.attackspeed + "(s)";
                infoUItext[3].text = "Range - " + mec.range;
                infoUItext[4].text = "Bullet Speed - " + mec.bulletspeed;
                infoUItext[5].text = "DPS - " + Mathf.Round((mec.basedamage * (1 / mec.attackspeed)) * 10) * 0.1f;
            }
        }
    } 
}
