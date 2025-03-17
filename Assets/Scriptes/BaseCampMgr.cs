using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseCampMgr : MonoBehaviour
{

    public GameObject hpbar;
    private Image hp;
    private TMP_Text hp_txt;

    public int maxhealth = 100;
    public int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        hp = hpbar.transform.GetChild(1).GetComponent<Image>();
        hp_txt = hpbar.transform.GetChild(2).GetComponent<TMP_Text>();
        updateHpbar(health);
    }

    private void LateUpdate()
    {
        hpbar.transform.forward = Camera.main.transform.forward;
    }
    public void updateHpbar(int health)
    {
        if (hp == null) return;
        if (health <= 0)
        {
            if (GameMgr.Instance != null) GameMgr.Instance.Gameover();
        }
        hp.fillAmount = (float)health / (float)maxhealth;
        hp_txt.text = string.Format("{0}/{1}", health, maxhealth);
    }
    public void updateHpbar_ui(Image hp, TMP_Text hp_txt)
    {
        if (hp == null) return;
        hp.fillAmount = (float)health / (float)maxhealth;
        hp_txt.text = string.Format("{0}/{1}", health, maxhealth);
    }
}
