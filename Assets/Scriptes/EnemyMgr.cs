using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;


public class EnemyMgr : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    public GameObject hpbar;
    private Image hp;
    private TMP_Text hp_txt;
    NavMeshAgent agent;
    Vector3 dir;

    public float maxhealth = 20;
    public float health = 20;
    public float movspeed = 1.5f;
    public float attackdamage = 10f;
    public int given_money = 200;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Basecamp").gameObject.transform;
        hp = hpbar.transform.GetChild(1).GetComponent<Image>();
        hp_txt = hpbar.transform.GetChild(2).GetComponent<TMP_Text>();
        agent = GetComponent<NavMeshAgent>();
        dir = agent.destination;
        agent.speed = movspeed;
        maxhealth *= (int)(1 +GameMgr.Instance.getmagnific() * (1 + GameMgr.Instance.getstage()/10));
        health = maxhealth;
        updateHpbar((int)Mathf.Round(health));
    }

    // Update is called once per frame
    void Update()
    {
        dir = target.position;
        agent.destination = dir;
    }

    private void LateUpdate()
    {
        hpbar.transform.forward = Camera.main.transform.forward;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("bullet") && GameMgr.Instance != null)
        {
            GameMgr.Instance.damageMgr(gameObject, other.gameObject);
        }
        else if (other.CompareTag("base") && GameMgr.Instance != null)
        {
            GameMgr.Instance.getbaseDamageMgr(gameObject, other.gameObject);
        }
    }
    public void updateHpbar(int health)
    {
        if (hp == null) return;
        if (health <= 0)
        {
           // GameMgr.Instance.leftenemy--;
            GameMgr.Instance.settextCoinUI(given_money);
            Destroy(gameObject);
        }
        hp.fillAmount = (float)health/(float)maxhealth;
        hp_txt.text = string.Format("{0}/{1}", health, maxhealth);

    }
}
