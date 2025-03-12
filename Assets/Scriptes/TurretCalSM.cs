using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurretCalSM : MonoBehaviour
{
    public TMP_Text[] infoUItext = new TMP_Text[6];
    public void updatturretinfo(MecMgr mec)
    {
        if (mec == null) return;
        string tempstring = "?";
        if (mec.mectype == 1) tempstring = "";
        else if (mec.mectype == 2) tempstring = "Heavy ";
        else if (mec.mectype == 3) tempstring = "Sniper ";
        infoUItext[0].text = "<"+ tempstring + "Turret LV " + "1" + ">";
        infoUItext[1].text = "Damage - " + mec.basedamage;
        infoUItext[2].text = "Attack Speed - " + mec.attackspeed +"(s)";
        infoUItext[3].text = "Range - " + mec.range;
        infoUItext[4].text = "Bullet Speed - " + mec.bulletspeed;
        infoUItext[5].text = "DPS - " + mec.basedamage*(1/mec.attackspeed);
    } 
}
