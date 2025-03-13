using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoshotSM : MonoBehaviour
{
    public GameObject selectedmec;
    public GameObject spinpoint;
    public GameObject target;
    public float speed =5f;
    private MecMgr mecmgr;
    // Start is called before the first frame update
    void Start()
    {
        if(spinpoint == null)
        {
            Debug.Log("no selected spinpoint C:16");
            return;
        }
        if (selectedmec != null) mecmgr = selectedmec.GetComponent<MecMgr>();
        else
        {
            Debug.Log("no selected mec C:22");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if (target != null) {
            Vector3 dir = new Vector3(target.transform.position.x, spinpoint.transform.position.y, target.transform.position.z) - spinpoint.transform.position;
            spinpoint.transform.rotation = Quaternion.Lerp(spinpoint.transform.rotation, Quaternion.LookRotation(-dir), Time.deltaTime * speed);
            mecmgr.setdir(spinpoint.transform.forward);
            mecmgr.setfireable(true);
        }
        else mecmgr.setfireable(false);

    }

    private void OnTriggerStay(Collider other)
    {
        if (target == null)
        {
            
            if (other.CompareTag("enemy") && other.gameObject != null)
            {
               target = other.gameObject;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (target != null)
        {
           target = null;
        }
    }
}
