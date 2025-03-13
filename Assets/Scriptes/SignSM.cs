using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class SignSM : MonoBehaviour
{
    private void LateUpdate()
    {
        gameObject.transform.forward = Camera.main.transform.forward;
    }
}
