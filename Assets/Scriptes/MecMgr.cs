using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MecMgr : MonoBehaviour
{
    public int mectype = 0;
    public bool hasautoattack = false;
    public Transform[] gunshotplace;
    public GameObject attackarea;
    public GameObject ball;
    public GameObject stats;
    public int upgradelevel = 0;
    public int maxupgradelevel = 0;
    private turretUpgradeInfo turretinfo;
    public float attackspeed;
    public float basedamage;
    public float bulletspeed;
    public float range;
    private bool fireable = true;
    private Vector3 dir;
    private bool iscooltime = false;
    public Animator ani;

    private int firenum = 0;

    private static string[] turretname= {
        "Turret",
        "Heavy Turret",
        "Sniper Turret",
        "Auto Turret"};
    public class turretUpgradeInfo {
       private int maxupgradlevel;
       public float[] t_attackspeed;
       public float[] t_basedamage;
       public float[] t_bulletspeed;
       public float[] t_range;
       public int[] t_upgradecost;
       public int t_totalcost;
        public turretUpgradeInfo()
        {
            maxupgradlevel = 3;
            t_attackspeed = new float[maxupgradlevel];
            t_basedamage = new float[maxupgradlevel];
            t_bulletspeed = new float[maxupgradlevel];
            t_range = new float[maxupgradlevel];
            t_upgradecost = new int[maxupgradlevel];
            t_totalcost = 0;
        }
        public void setupgradeStats(float sp, float dmg, float btsp, float rg, int curlvl, int cost)
        {
            if (maxupgradlevel < 0) return;
            t_attackspeed[curlvl] = sp;
            t_basedamage[curlvl] = dmg;
            t_bulletspeed[curlvl] = btsp;
            t_range[curlvl] = rg;
            t_upgradecost[curlvl] = cost;
        }

        public int getmaxupgradelevel()
        {
            return maxupgradlevel;
        }
    }

    private void Start()
    {
        if (attackarea != null) attackarea.SetActive(false);
        upgradelevelSM();
        turretinfo = new turretUpgradeInfo();
        setturretupgradeStats(mectype, turretinfo);
        StartCoroutine(coupdate());
    }

    public void turretUpgradeEvent()
    {
        if (GameMgr.Instance == null || turretinfo == null) return;
        GameMgr.Instance.settextCoinUI(-turretinfo.t_upgradecost[upgradelevel]);
        turretinfo.t_totalcost += turretinfo.t_upgradecost[upgradelevel];
        ++upgradelevel;
        basedamage = turretinfo.t_basedamage[upgradelevel];
        bulletspeed = turretinfo.t_bulletspeed[upgradelevel];
        attackspeed = turretinfo.t_attackspeed[upgradelevel];
        range = turretinfo.t_range[upgradelevel];
        upgradelevelSM();
    }

    private void setturretupgradeStats(int type, turretUpgradeInfo info)
    {
        maxupgradelevel = (info.getmaxupgradelevel()-1);
        if(GameMgr.Instance != null) info.t_totalcost = GameMgr.Instance.UI.GetComponent<UIMgr>().getmecprice()[type-1];
        if (turretinfo == null) return;
        // atksp, dmg, btsp, range, curlvl
        switch (type)
        {
            case 1: { //mec1
                    info.setupgradeStats(2f, 7f, 5f, 10f, 0, 2000);
                    info.setupgradeStats(1.8f, 9f, 7f, 10f, 1, 3750);
                    info.setupgradeStats(1.4f, 13.5f, 10f, 10f, 2, 4250);
                    break;
                }
            case 2: { //mec2
                    info.setupgradeStats(0.5f, 3f, 10f, 2f, 0, 4750);
                    info.setupgradeStats(0.45f, 4f, 11f, 3f, 1, 9350);
                    info.setupgradeStats(0.35f, 5f, 13f, 4f, 2, 12000);
                    break;
                }
            case 3: { //mec3
                    info.setupgradeStats(2.5f, 15f, 25f, 35f, 0, 7800);
                    info.setupgradeStats(2.3f, 25f, 30f, 35f, 1, 10750);
                    info.setupgradeStats(1.7f, 45f, 40f, 35f, 2, 13000);
                    break;
                }
            case 4: { //mec4
                    info.setupgradeStats(1.8f, 9f, 12f, 15f, 0, 8500);
                    info.setupgradeStats(1.6f, 12f, 15f, 15f, 1, 11500);
                    info.setupgradeStats(1.2f, 18f, 20f, 15f, 2, 12500);
                    break;
                }
            default:
                break;
        }
    }

    private IEnumerator coupdate()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            if (!iscooltime && fireable)
            {
                if(hasautoattack) yield return new WaitForSeconds(0.5f);
                else if (!hasautoattack) dir = transform.forward;
                if (ani != null) ani.SetBool("fire", true);
                GameObject bullet;
                if (gunshotplace.Length >= 2)
                {
                    bullet = Instantiate(ball, gunshotplace[firenum].position, Quaternion.identity, transform);
                    bullet.SetActive(true);
                    firenum += 1;
                    if (firenum >= 2) firenum = 0;
                }
                else
                {
                    bullet = Instantiate(ball, gunshotplace[0].position, Quaternion.identity, transform);
                    bullet.SetActive(true);
                }
                StartCoroutine(coolTime(attackspeed));
                //bullet.transform.SetParent(transform);
            }
            yield return null;
        }
    }
    private void LateUpdate()
    {
        if (stats != null) stats.transform.forward = Camera.main.transform.forward;
    }

    public void upgradelevelSM()
    {
        if (upgradelevel >= 2) stats.transform.GetChild(0).GetComponent<TMP_Text>().text = turretname[mectype - 1] + " MAX";
        else stats.transform.GetChild(0).GetComponent<TMP_Text>().text = turretname[mectype - 1] + " LV " + upgradelevel;
    }


    IEnumerator coolTime(float t)
    {
        iscooltime = true;
        yield return new WaitForSeconds(t);
        if (ani != null) ani.SetBool("fire", false);
        iscooltime = false;

    }

    public Vector3 getdir()
    {
        return dir;
    }
    public void setdir(Vector3 d) {
        dir = d;
    }

    public void setfireable(bool b)
    {
        fireable = b;
    }
    public turretUpgradeInfo getturretinfo()
    {
        return turretinfo;
    }

}
