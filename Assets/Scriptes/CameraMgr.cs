using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    [SerializeField]
    float camspeed = 8f;
    Vector3 mov = Vector3.zero;
    private int ang = 0;
    private int altitude = 0;
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            mov = new Vector3(-camspeed,0,0);
            if (ang == 0) transform.position += mov * Time.deltaTime;
            else transform.position -= mov * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
           mov = new Vector3(camspeed,0,0);
            if (ang == 0) transform.position += mov * Time.deltaTime;
            else transform.position -= mov * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            mov = new Vector3(0, 0, camspeed);
            if (ang == 0) transform.position += mov * Time.deltaTime;
            else transform.position -= mov * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            mov = new Vector3(0, 0, -camspeed);
            if (ang == 0) transform.position += mov * Time.deltaTime;
            else transform.position -= mov * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            mov = new Vector3(0, -camspeed ,0);
            transform.position += mov * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            mov = new Vector3(0, camspeed, 0);
            transform.position += mov * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            ang += 180;
            if (ang >= 360) ang = 0;
            transform.rotation = Quaternion.Euler(new Vector3(60, ang, 0));
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            altitude += 10;
            transform.position = new Vector3(transform.position.x, altitude, transform.position.z);
            if (altitude >= 30) altitude = 0;  
        }

    }

    public void movetoSelectedTurret(Transform turret)
    {
        transform.position = new Vector3(turret.position.x-0.5f, turret.position.y+8f, turret.position.z-4);
    }
}
