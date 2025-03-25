using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class GameMgr : MonoBehaviour
{
    private static GameMgr instance;
    public GameObject UI;
    public GameObject basecamp;
    public TMP_Text[] LevelSM;
    
    private Transform turrets;
    private Transform Enemys;
    private int turretcount = 0;
    private int turretcount_max = 10;

    public GameObject[] enemy;
    public Transform[] enemyspawnloactions;
    public int leftenemy = 0;
    private GameObject[] gui = new GameObject[2];

    [SerializeField]
    private int coins = 0;
    private int tempsave_mecprice = 0;
    private int fastsetmode = 1;


    private bool isinstallmode = false;

    private int stage = 1;
    private int wave = 0;
    private int maxwave = 0;
    private float magnific = 1;

    [SerializeField]
    private int roundtimer = 0;

    private bool isgameview = false;

    private void Awake()
    {
        if (null == instance) { 
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += LoadedsceneEvent;
            Screen.SetResolution(1920, 1080, true);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void Gameover()
    {
        isgameview = false;
        SceneManager.LoadScene("Title");
        gameObject.SetActive(false);
    }
   
    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name + "으로 변경되었습니다.");
        if (SceneManager.GetActiveScene().name.Equals("SampleScene"))
        {
            gameObject.SetActive(true);
            isgameview = true;
            unitgameSystem();
        }
        else
        {
            isgameview = false;
            gameObject.SetActive(false);
        }
    }
    private void unitgameSystem()
    {
        if (turrets == null) turrets = GameObject.Find("Turrets").gameObject.transform;
        if (Enemys == null) Enemys = GameObject.Find("Enemys").gameObject.transform;
        if (LevelSM[0] == null) LevelSM[0] = GameObject.Find("stage_txt").GetComponent<TMP_Text>();
        if (LevelSM[1] == null) LevelSM[1] = GameObject.Find("round_txt").GetComponent<TMP_Text>();
        if (enemyspawnloactions[0] == null) enemyspawnloactions[0] = GameObject.Find("Enemy SpawnPlace").transform;
        coins = 0; tempsave_mecprice = 0; roundtimer = 0;
        stage = 1; wave = 0; maxwave = 0; magnific = 1; isinstallmode = false;
        turretcount = 0;
        fastsetmode = 1;
        Time.timeScale = fastsetmode;
        StartCoroutine(GameStart());
    }
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(0.1f);
        if (TitleMgr.Instance != null)
        {
            TitleMgr.Instance.gameObject.SetActive(true);
            TitleMgr.Instance.StartCoroutine(TitleMgr.Instance.ingameSettingSM());
        }
        switch (stage)
        {
            case 1: {
                    settextCoinUI(1000);
                    setbasecampUI(200);
                    roundtimer = 30;
                    leftenemy = 5;
                    wave = 0;
                    maxwave = 1;
                    magnific = 0.5f;
                    break; }
            case 2:
                {
                    roundtimer = 15;
                    leftenemy = 5;
                    wave = 0;
                    maxwave = 1;
                    magnific = 0.7f;
                    break;
                }
            case 3:
                {
                    roundtimer = 15;
                    leftenemy = 7;
                    wave = 0;
                    maxwave = 1;
                    magnific = 0.8f;
                    break;
                }
            case 4:
                {
                    roundtimer = 15;
                    leftenemy = 9;
                    wave = 0;
                    maxwave = 1;
                    magnific = 0.8f;
                    break;
                }
            case 8:
                {
                    roundtimer = 15;
                    leftenemy = 12;
                    wave = 0;
                    maxwave = 1;
                    magnific = 0.9f;
                    break;
                }
            case 10:
                {
                    roundtimer = 15;
                    leftenemy = 15;
                    wave = 0;
                    maxwave = 1;
                    magnific = 1.25f;
                    break;
                }
            default: {
                    roundtimer = 15;
                    leftenemy = 10;
                    wave = 0;
                    maxwave = 1;
                    break; }
        }
        LevelSM[0].text = "Stage " + stage;
        while (roundtimer >= 0 && isgameview)
        {
            LevelSM[1].text = "Next Wave : " + roundtimer;
            yield return new WaitForSeconds(1f);
            roundtimer--;
            if (!isgameview) yield break;
        }
        roundtimer = 0;
        // LevelSM[1].text = "Wave " + wave + "/" + maxwave;
        LevelSM[1].text = "Start Wave";
        yield return new WaitForSeconds(1.5f);
        while (wave < maxwave && isgameview)
        {
            wave++;
            LevelSM[1].text = "Wave " + wave + "/" + maxwave;
            if (!isgameview) yield break;
            while (leftenemy > 0 && isgameview)
            {
                enemySpawnSM(0);
                yield return new WaitForSeconds(3.5f);
                if (!isgameview) yield break;
                yield return new WaitForSeconds(0.3f);
                if (stage >= 2) {
                    enemySpawnSM(1);
                    yield return new WaitForSeconds(2.5f);
                }
                if (stage >= 4) {
                    enemySpawnSM(2);
                    yield return new WaitForSeconds(2.8f);
                }
                if (stage >= 6) {
                    enemySpawnSM(3);
                    yield return new WaitForSeconds(2.7f);
                }
                if (stage >= 8)
                {
                    enemySpawnSM(4);
                    yield return new WaitForSeconds(1.9f);
                }
                if (stage >= 10) {
                    enemySpawnSM(5);
                    yield return new WaitForSeconds(2.6f);
                }
                if (stage >= 12)
                {
                    enemySpawnSM(6);
                    yield return new WaitForSeconds(2.6f);
                }
                yield return null;
            }
            yield return null;
            if(wave < maxwave)yield return new WaitForSeconds(3f);
        }
        stage++;
        StartCoroutine(GameStart());
        
    }

    private void enemySpawnSM(int num) //Min 0 ~ Max 5 index
    {
        if (!isgameview) return;
        leftenemy--;
        Instantiate(enemy[num], enemyspawnloactions[0].position, Quaternion.identity, Enemys);
    }

    public static GameMgr Instance
    {
        get
        {
            if(null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    public void settextCoinUI(int v)
    {
        if(UI == null) UI = GameObject.Find("GameUI");
        if(UI != null)
        {
            coins += v;
            if (coins >= 999999) coins = 999999;
            UI.GetComponent<UIMgr>().UpdatetextCoinUI();
        }
    }
    public void settextTurretcountUI(int v)
    {
        if (UI == null) UI = GameObject.Find("GameUI");
        if (UI != null)
        {
            turretcount += v;
            UI.GetComponent<UIMgr>().UpdatetextTurretsUI();
        }
    }
    public void settextTurretcountmaxUI(int v)
    {
        if (UI == null) UI = GameObject.Find("GameUI");
        if (UI != null)
        {
            turretcount_max += v;
            UI.GetComponent<UIMgr>().UpdatetextTurretsUI();
        }
    }
    public void setbasecampUI(int v)
    {
        if (basecamp == null) basecamp = GameObject.Find("Basecamp");
        if (basecamp != null)
        {
            basecamp.GetComponent<BaseCampMgr>().health = v;
            basecamp.GetComponent<BaseCampMgr>().updateHpbar(v);
        }
    }
    public int getcoins()
    {
        return coins;
    }

    public void mecUI(bool b)
    {
        if (UI == null) UI = GameObject.Find("GameUI");
        if (UI != null)
        {
          UI.GetComponent<UIMgr>().setactivemecUI(b);
        }
    }
    public void getbaseDamageMgr(GameObject my, GameObject basecamp)
    {
        if (my == null || basecamp == null) return;
        BaseCampMgr basecampmgr = basecamp.GetComponent<BaseCampMgr>();
        EnemyMgr enemymgr = my.GetComponent<EnemyMgr>();
        float takendmg = enemymgr.attackdamage;
        int basecamphealth = basecampmgr.health;
        int cal = (int)(basecamphealth - takendmg);
        basecampmgr.health = cal;
        basecampmgr.updateHpbar(cal);
        UI.GetComponent<UIMgr>().updatedUIbasecamp();
        Destroy(my);
    }

    public void damageMgr(GameObject my, GameObject bullet)
    {
        //Debug.Log(my + "" + bullet);
        if (my == null || bullet == null) return;
        bullet.SetActive(false);
        BallMov ballmov = bullet.GetComponent<BallMov>();
        EnemyMgr enemymgr = my.GetComponent<EnemyMgr>();
        float takendmg = ballmov.getdamage();
        float enemyhealth = (enemymgr.health);
        int cal = (int)(enemyhealth - takendmg);
        enemymgr.health = cal;
        enemymgr.updateHpbar(cal);
        Destroy(bullet);
    }

    public IEnumerator TurretrUpgradeCilckEvent(GameObject my, GameObject turret, GameObject[] gui)
    {
        bool upgradedisplaymode = false;
        bool refundisplaymode = false;
        InfoMgr tr_info = gui[0].GetComponent<InfoMgr>();
        TurretCalSM tr_cal = gui[1].GetComponent<TurretCalSM>();
        tr_info.turretgrademode();
        tr_info.apperupgradecost();
        MecMgr mec = turret.GetComponent<MecMgr>();
        mec.stats.SetActive(false);
        if (mec.attackarea != null) mec.attackarea.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        bool end = false;
        while (!end && isgameview)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (mec.upgradelevel < mec.maxupgradelevel)
                {
                    upgradedisplaymode = true;
                    tr_cal.isupgrademode = true;
                    tr_cal.updatturretinfo(mec);
                    end = true;
                }
            }
            else if (Input.GetMouseButtonDown(0) || Time.timeScale == 0)
            {
                for(int i=0; i<gui.Length; i++) Destroy(gui[i]);
                mec.stats.SetActive(true);
                if (mec.attackarea != null) mec.attackarea.SetActive(false);
                end = true;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                refundisplaymode = true;
                end = true;
            }
            yield return null;
         }
        if(upgradedisplaymode)
        {
            end = false;
            tr_info.isupgardemode = true;
            tr_info.turretgrademode();
            while (!end && isgameview)
            {
                if (Input.GetKeyDown(KeyCode.F) && coins >= mec.getturretinfo().t_upgradecost[mec.upgradelevel])
                {
                    mec.GetComponent<MecMgr>().turretUpgradeEvent();
                    for (int i = 0; i < gui.Length; i++) Destroy(gui[i]);
                    mec.stats.SetActive(true);
                    if (mec.attackarea != null) mec.attackarea.SetActive(false);
                    end = true;
                }
                else if (Input.GetMouseButtonDown(0))
                {
                    for (int i = 0; i < gui.Length; i++) Destroy(gui[i]);
                    mec.stats.SetActive(true);
                    if (mec.attackarea != null) mec.attackarea.SetActive(false);
                    end = true;
                }
                yield return null;
            }
        }
        else if (refundisplaymode) {
            end = false;
            tr_info.isrefundmode = true;
            tr_info.turretgrademode();
            while (!end && isgameview)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    for (int i = 0; i < gui.Length; i++) Destroy(gui[i]);
                    mec.stats.SetActive(true);
                    if (mec.attackarea != null) mec.attackarea.SetActive(false);
                    end = true;
                }
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    tempsave_mecprice = mec.getturretinfo().t_totalcost;
                    Destroy(turret);
                    for (int i = 0; i < gui.Length; i++) Destroy(gui[i]);
                    tempsave_mecprice = ((tempsave_mecprice * 50) / 100);
                    refund_turret();
                    tempsave_mecprice = 0;
                    settextTurretcountUI(-1);
                    end = true;
                }
                yield return null;
            }
        }
        if (Time.timeScale != 0) mecUI(true);
                my.SetActive(true);
                my.GetComponent<SelectMgr>().StartCoroutine(my.GetComponent<SelectMgr>().coupdate());
    }

    public Transform getturrets()
    {
        return turrets;
    }

    public Transform getenemys()
    {
        return Enemys;
    }
    public int gettempsave_mecprice()
    {
        return tempsave_mecprice;
    }
    public void settempsave_mecprice(int i)
    {
        tempsave_mecprice = i;
    }
    public void refund_turret()
    {
        settextCoinUI(tempsave_mecprice);
        
    }
    public GameObject[] getgui()
    {
        return gui;
    }
    public void setgui(GameObject[] g)
    {
        gui = g;
    }

    public bool getisinstallmode()
    {
        return isinstallmode;
    }
    public void setisinstallmode(bool b)
    {
        isinstallmode = b;
    }
    public float getmagnific()
    {
        return magnific;
    }
    public int getstage()
    {
        return stage;
    }
    public int getturretcount()
    {
        return turretcount;
    }
    public int getturretcountmax()
    {
        return turretcount_max;
    }
    public int getfastsetmode()
    {
        return fastsetmode;
    }
    public void setfastsetmode(int i)
    {
        fastsetmode = i;
    }
}
