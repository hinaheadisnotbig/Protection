using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MecMgr : MonoBehaviour
{
    public int mectype = 1;
    public Transform[] gunshotplace;
    public GameObject attackarea;
    public GameObject ball;
    public GameObject stats;
    public float attackspeed = 1f;
    public float basedamage = 2.5f;
    public float bulletspeed = 5f;
    public float range = 3f;
    private bool fireable = true;
    private Vector3 dir;
    private bool iscooltime = false;
    public Animator ani;

    private int firenum = 0;

    private void Start()
    {
        if (attackarea != null) attackarea.SetActive(false);
        StartCoroutine(coupdate());
    }

    private IEnumerator coupdate()
    {
        yield return new WaitForSeconds(0.5f);
        while (true)
        {
            if (!iscooltime && fireable)
            {
                if (ani != null) ani.SetBool("fire", true);
                if (mectype != 3) dir = transform.forward;
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

}
