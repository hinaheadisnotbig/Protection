using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TurretCalSM : MonoBehaviour
{
    public TMP_Text[] infoUItext = new TMP_Text[6];
    public void updatturretinfo(MecMgr mec)
    {
        if (mec == null) return;
        string tempstring;
        if (mec.stats != null) tempstring = mec.stats.transform.GetChild(0).GetComponent<TMP_Text>().text;
        else tempstring = "<Turret LV 1>";
        infoUItext[0].text = "<"+ tempstring + ">";
        infoUItext[1].text = "Damage - " + mec.basedamage;
        infoUItext[2].text = "Attack Speed - " + mec.attackspeed +"(s)";
        infoUItext[3].text = "Range - " + mec.range;
        infoUItext[4].text = "Bullet Speed - " + mec.bulletspeed;
        infoUItext[5].text = "DPS - " + Mathf.Round((mec.basedamage*(1/mec.attackspeed)) * 10) * 0.1f;
    } 
}
