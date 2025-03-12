using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SelectMgr : MonoBehaviour
{
    public GameObject turret_info;
    public LayerMask turretmask;
    private GameObject target;
    private GameObject SelectedTurret;
    private bool isClick = false;
    // Start is called before the first frame update
    void Start()
    {
        turretmask = 1 << LayerMask.NameToLayer("TURRET");
        StartCoroutine(coupdate());
    }

   
    public IEnumerator coupdate()
    {
        isClick = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        target = gameObject;//Instantiate(targetMark, hit.point, targetMark.transform.rotation);
        while (!isClick)
        {
            Ray rayTarget = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitTarget;
            if (Physics.Raycast(rayTarget, out hitTarget, Mathf.Infinity, turretmask))
            {
                transform.position = hitTarget.point + new Vector3(0, 0.75f, 0);
                if (Input.GetMouseButtonDown(0) && isClick == false && SelectedTurret != null) {
                    
                    isClick = true;
                    GameMgr.Instance.mecUI(false);
                    GameObject gui = Instantiate(turret_info, GameMgr.Instance.getturrets());
                    gui.GetComponent<InfoMgr>().install = SelectedTurret.transform.gameObject;
                    GameMgr.Instance.StartCoroutine(GameMgr.Instance.TurretrUpgradeCilckEvent(gameObject, SelectedTurret, gui));
                    SelectedTurret = null;
                    gameObject.SetActive(false);
                }
            } else transform.position = hitTarget.point + new Vector3(0, 10, 0);
            yield return null;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("turret"))
        {
            SelectedTurret = other.gameObject;
           // Debug.Log(SelectedTurret);
        }
    }
}
