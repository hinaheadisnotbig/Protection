using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class InstallMgr : MonoBehaviour
{
    Renderer cubeColor;
    private Color green;
    private Color red;
    private int ang = 0;
    private bool isinstallable = false;

    private GameObject target;

    public GameObject mecObj;
    public float y_offset = 0.75f;
    public LayerMask layermask;
    public LayerMask nonemask;
    private static bool isClick = false;
    void Start()
    {
        GameMgr.Instance.mecUI(false);
        layermask = 1 << LayerMask.NameToLayer("FLOOR");
        nonemask = 1 << LayerMask.NameToLayer("NONE");
        cubeColor = GetComponent<Renderer>();
        green = new Color(0 / 255f, 255 / 255f, 0 / 255f, 163/255f);
        red = new Color(255 / 255f, 0 / 255f, 0 / 255f, 163/255f);
        installmec();
    }

    public void SpawnMissleObj()
    {
        StartCoroutine(coSpawnMissleObj());
    }

    private IEnumerator coupdate()
    {
        yield return new WaitForSeconds(0.1f);
        while(!isClick)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ang += 90;
                if (ang > 360) ang = 90;
                transform.rotation = Quaternion.Euler(new Vector3(0, ang, 0));
            }
            else if (target != null && isClick == false && Input.GetMouseButtonDown(0) && cubeColor.material.color == green)
            {
                isClick = true;
                SpawnMissleObj();
                Destroy(target); Destroy(GameMgr.Instance.getgui());
                GameMgr.Instance.mecUI(true);
                GameMgr.Instance.settempsave_mecprice(0);
            }
            else if (Input.GetKeyDown(KeyCode.X) && !isClick || target != null && isClick == false && Input.GetMouseButtonDown(0) && cubeColor.material.color == red)
            {
                isClick = true;
                Destroy(target); Destroy(GameMgr.Instance.getgui());
                GameMgr.Instance.mecUI(true);
                GameMgr.Instance.refund_turret();
                GameMgr.Instance.settempsave_mecprice(0);
            }
            yield return null;
        }
    }

    IEnumerator coSpawnMissleObj()
    {
        if (isClick)
        {
            GameObject install = Instantiate(mecObj, target.transform.GetChild(0).position,transform.rotation, GameMgr.Instance.getturrets());
        }
        yield return null;
    }
    public void installmec()
    {
        StartCoroutine(cospawnTarget());
    }
    IEnumerator cospawnTarget()
    {
        isinstallable = false;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(coupdate());
        isClick = false;
        // 카메라에서 마우스 위치로 레이를 쏜다
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        // 지정된 레이어에 맞으면

        //if (Physics.Raycast(ray, out hit, Mathf.Infinity, layermask))
        //{
            target = gameObject;//Instantiate(targetMark, hit.point, targetMark.transform.rotation);
            while (!isClick)
            {
                Ray rayTarget = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitTarget;
            
            if (Physics.Raycast(rayTarget, out hitTarget, Mathf.Infinity, layermask))
            {
                // 마우스 위치로 계속 좌표 위치를 변환시켜줌
                if (!isinstallable) cubeColor.material.color = red;
                else cubeColor.material.color = green;
                transform.position = hitTarget.point + new Vector3(0, y_offset, 0);
            }
            else if (Physics.Raycast(rayTarget, out hitTarget, Mathf.Infinity, nonemask))
            {
                cubeColor.material.color = red;
                transform.position = hitTarget.point + new Vector3(0, y_offset, 0);
            }
            yield return null;
        }
       // }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("none"))
        {
            //Debug.Log("1");
            isinstallable = false;
            Ray rayTarget = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitTarget;
            if (Physics.Raycast(rayTarget, out hitTarget, Mathf.Infinity, layermask))
            {
                transform.position = hitTarget.point + new Vector3(0, y_offset, 0);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
       isinstallable = true;
    }



}
