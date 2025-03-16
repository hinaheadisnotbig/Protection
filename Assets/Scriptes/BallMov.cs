using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMov : MonoBehaviour
{
    private float movspeed = 5f;
    private float damage = 0f;
    private float range = 3f;
    private MecMgr mecMgr;
    private Vector3 dir;
    private void Start()
    {
        mecMgr = transform.parent.GetComponent<MecMgr>();
        damage = mecMgr.basedamage;
        movspeed = mecMgr.bulletspeed;
        this.range = mecMgr.range;
        dir = mecMgr.getdir();
        StartCoroutine(removeobj());
        //transform.position += transform.TransformDirection(-dir) * 1.5f;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.TransformDirection(-dir) * Time.deltaTime * movspeed;
    }

    IEnumerator removeobj()
    {
        yield return new WaitForSeconds(range);
        //Debug.Log("»ç¶óÁü");
        Destroy(gameObject);
    }

    public float getdamage()
    {
        return damage;
    }
    public float getmovspeed()
    {
        return movspeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("none"))
        {
            //Debug.Log("???");
            Destroy(gameObject);
        }
    }
}
