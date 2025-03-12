using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;

public class UIMgr : MonoBehaviour
{
    public GameObject[] mectype;
    public int[] mecprice = { 
        1000,
        2500,
    };
    public GameObject gui_prefab;
    private Image[] btn;
    private GameObject[] UI_basecamp = new GameObject[2];
    private GameObject mecpanel;
    private TMP_Text txt;
    private Color gray;
    private Color white;


    private void Awake()
    {
        gray = new Color(155 / 255f, 155 / 255f, 155 / 255f, 255/255f);
        white = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255/255f);
        btn = new Image[mecprice.Length];
        txt = transform.GetChild(0).GetChild(0).gameObject.GetComponent<TMP_Text>();
        for (int i = 0; i < UI_basecamp.Length; i++) UI_basecamp[i] = transform.GetChild(1).GetChild(0).GetChild(i+1).gameObject;
        for (int i = 0; i < btn.Length; i++) btn[i] = transform.GetChild(2).GetChild(i).gameObject.GetComponent<Image>();
        mecpanel = transform.gameObject;//transform.GetChild(2).gameObject;
    }

    public void updatedUIbasecamp()
    {
        if (GameMgr.Instance.basecamp == null) return;
        GameMgr.Instance.basecamp.GetComponent<BaseCampMgr>().updateHpbar_ui(UI_basecamp[0].GetComponent<Image>(), UI_basecamp[1].GetComponent<TMP_Text>());
    }
  
    public void buymec(int op)
    {
        if (GameMgr.Instance.getcoins() >= mecprice[op])
        {
            GameObject install = Instantiate(mectype[op], GameMgr.Instance.getturrets());
            GameObject gui = Instantiate(gui_prefab, GameMgr.Instance.getturrets());
            gui.GetComponent<InfoMgr>().install = install;
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

    }

    public void setactivemecUI(bool b)
    {
        mecpanel.SetActive(b);
    }
}
