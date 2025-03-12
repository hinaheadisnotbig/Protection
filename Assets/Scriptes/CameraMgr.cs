using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : MonoBehaviour
{
    [SerializeField]
    float camspeed = 8f;
    Vector3 mov = Vector3.zero;
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            mov = new Vector3(-camspeed,0,0);
            transform.position += mov * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))
        {
           mov = new Vector3(camspeed,0,0);
           transform.position += mov * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            mov = new Vector3(0, 0, camspeed);
            transform.position += mov * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            mov = new Vector3(0, 0, -camspeed);
            transform.position += mov * Time.deltaTime;
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
    }
}
