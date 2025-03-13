using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameMgr : MonoBehaviour
{
    private static GameMgr instance;
    public GameObject UI;
    public GameObject basecamp;
    private Transform turrets;
    private Transform Enemys;
    public GameObject[] enemy;
    public Transform[] enemyspawnloactions;

    [SerializeField]
    private int coins = 0;
    private int tempsave_mecprice = 0;

    private GameObject[] gui = new GameObject[2];

    private bool isinstallmode = false;

    private void Awake()
    {
        if (null == instance) { 
            instance = this;
            DontDestroyOnLoad(gameObject);

            settingsystem();
        } else
        {
            Destroy(gameObject);
        }
    }

    private void settingsystem()
    {
        if(turrets == null) turrets = GameObject.Find("Turrets").gameObject.transform;
        if(Enemys == null)  Enemys = GameObject.Find("Enemys").gameObject.transform;

        StartCoroutine(GameStart());

    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(0.1f);
        settextCoinUI(5000);
        setbasecampUI(200);
        yield return new WaitForSeconds(2.5f);
        while(true)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1)) Instantiate(enemy[0], enemyspawnloactions[0].position, Quaternion.identity, Enemys);
            if (Input.GetKeyDown(KeyCode.Alpha2)) Instantiate(enemy[1], enemyspawnloactions[0].position, Quaternion.identity, Enemys);
            if (Input.GetKeyDown(KeyCode.Alpha3)) Instantiate(enemy[2], enemyspawnloactions[0].position, Quaternion.identity, Enemys);
            /*yield return new WaitForSeconds(5f);
            Instantiate(enemy[0], new Vector3(0.389560729f, 1.00000095f, -22.6599998f), Quaternion.identity,Enemys);
            yield return new WaitForSeconds(7f);*/
            yield return null;
        }
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
            UI.GetComponent<UIMgr>().UpdatetextCoinUI();
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
        int enemyhealth = enemymgr.health;
        int cal = (int)(enemyhealth - takendmg);
        enemymgr.health = cal;
        enemymgr.updateHpbar(cal);
        Destroy(bullet);
    }

    public IEnumerator TurretrUpgradeCilckEvent(GameObject my, GameObject turret, GameObject[] gui)
    {
        MecMgr mec = turret.GetComponent<MecMgr>();
        mec.stats.SetActive(false);
        if (mec.attackarea != null) mec.attackarea.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        bool end = false;
        while (!end)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("¹Ì¿Ï");
            }
            else if (Input.GetMouseButtonDown(0))
            {
                for(int i=0; i<gui.Length; i++) Destroy(gui[i]);
                mec.stats.SetActive(true);
                if (mec.attackarea != null) mec.attackarea.SetActive(false);
                end = true;
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                Destroy(turret);
                for (int i = 0; i < gui.Length; i++) Destroy(gui[i]);
                tempsave_mecprice = UI.GetComponent<UIMgr>().getmecprice()[mec.mectype - 1];
                tempsave_mecprice = ((tempsave_mecprice * 50) / 100);
                refund_turret();
                tempsave_mecprice = 0;
                end = true;
            }
            yield return null;
         }
                mecUI(true);
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
}
