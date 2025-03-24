using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;
using Newtonsoft.Json.Linq;
using UnityEngine.SceneManagement;

public class UIMgr : MonoBehaviour
{
    public bool isabletopview = true;
    public GameObject Turretshop;
    public GameObject Baseshop;
    public GameObject TopView;
    public GameObject[] mectype;
    private int[] mecprice = { 
        1000,
        2500,
        3000,
        4750,
        0
    };
    private int[] upgradeuiprice = {
        5000,
        10000,
        25000
    };
    public GameObject[] gui_prefab = new GameObject[2];
    private Image[] btn;
    private Image[] upgradeui_btn;
    private GameObject[] UI_basecamp = new GameObject[2];
    private GameObject mecpanel;
    private TMP_Text txt;
    private TMP_Text turretcounttxt;
    private Color gray;
    private Color white;
    private TMP_Text[] btnprice_txt;
    private TMP_Text[] upgradeui_btnprice_txt;


    private void Awake()
    {
        gray = new Color(155 / 255f, 155 / 255f, 155 / 255f, 255/255f);
        white = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255/255f);
        btn = new Image[mecprice.Length];
        for (int i = 0; i < btn.Length; i++) btn[i] = Turretshop.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>();
        upgradeui_btn = new Image[upgradeuiprice.Length];
        for (int i = 0; i < upgradeui_btn.Length; i++) upgradeui_btn[i] = Baseshop.transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>();
        txt = transform.GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>();
        turretcounttxt = transform.GetChild(0).GetChild(1).gameObject.GetComponent<TMP_Text>();
        for (int i = 0; i < UI_basecamp.Length; i++) UI_basecamp[i] = transform.GetChild(1).GetChild(0).GetChild(i+1).gameObject;
        btnprice_txt = new TMP_Text[mecprice.Length];
        upgradeui_btnprice_txt = new TMP_Text[upgradeuiprice.Length];
        for (int i = 0; i < btnprice_txt.Length; i++)
        {
            btnprice_txt[i] = btn[i].transform.GetChild(0).GetComponent<TMP_Text>();
            btnprice_txt[i].text = mecprice[i].ToString() + " Coins";
        }
        for (int i = 0; i < upgradeui_btnprice_txt.Length; i++)
        {
           upgradeui_btnprice_txt[i] = upgradeui_btn[i].transform.GetChild(0).GetComponent<TMP_Text>();
           upgradeui_btnprice_txt[i].text = upgradeuiprice[i].ToString() + " Coins";
        }

        mecpanel = transform.gameObject;//transform.GetChild(2).gameObject;
        if (!isabletopview) TopView.SetActive(false);
        changeviewShop(0); //turret shop view
    }


    public void updatedUIbasecamp()
    {
        if (GameMgr.Instance.basecamp == null) return;
        GameMgr.Instance.basecamp.GetComponent<BaseCampMgr>().updateHpbar_ui(UI_basecamp[0].GetComponent<Image>(), UI_basecamp[1].GetComponent<TMP_Text>());
    }
  
    public void buymec(int op)
    {
        if (GameMgr.Instance.getcoins() >= mecprice[op] && GameMgr.Instance.getturretcount() < GameMgr.Instance.getturretcountmax())
        {
            GameObject install = Instantiate(mectype[op], GameMgr.Instance.getturrets());
            GameObject[] gui = new GameObject[2];
            for (int i = 0; i < gui.Length; i++)
            {
                gui[i] = Instantiate(gui_prefab[i], GameMgr.Instance.getturrets());
                gui[i].GetComponent<InfoMgr>().setinstallanother(install);
            }
            GameMgr.Instance.setgui(gui);
            GameMgr.Instance.settempsave_mecprice(mecprice[op]);
            GameMgr.Instance.settextCoinUI(-mecprice[op]);
        }
    }

    
    public void UpdatetextCoinUI()
    {
        if (GameMgr.Instance == null) return;
            txt.text = "Coins : " + GameMgr.Instance.getcoins();
            for (int i = 0; i < btn.Length; i++)
            {
                if (GameMgr.Instance.getcoins() >= mecprice[i]) btn[i].color = white;
                else btn[i].color = gray;
            }
            for (int i = 0; i < upgradeui_btn.Length; i++)
            {
                if (GameMgr.Instance.getcoins() >= upgradeuiprice[i]) upgradeui_btn[i].color = white;
                else upgradeui_btn[i].color = gray;
            }


    }
    public void UpdatetextTurretsUI()
    {
        if (GameMgr.Instance == null) return;
        turretcounttxt.text = "Turrets : " + GameMgr.Instance.getturretcount() + "/" + GameMgr.Instance.getturretcountmax();
    }

    public void setactivemecUI(bool b)
    {
        mecpanel.SetActive(b);
    }

    public int[] getmecprice()
    {
        return mecprice;
    }

    public void changeviewShop(int n)
    {
        switch (n)
        {
            case 0:
                {
                    Baseshop.transform.parent.parent.gameObject.SetActive(false);
                    Turretshop.transform.parent.parent.gameObject.SetActive(true);
                    break;
                }
            case 1:
                {
                    Baseshop.transform.parent.parent.gameObject.SetActive(true);
                    Turretshop.transform.parent.parent.gameObject.SetActive(false);
                    break;
                }
            default:
                break;
        }
        
    }
   
}
