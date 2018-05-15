using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    private Transform target;
    // Use this for initialization
    void Start()
    {
        target = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!InformationViewController.gravity_mode)
            transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position, target.transform.up);
        else
            transform.rotation = Quaternion.LookRotation(transform.position - target.transform.position);
    }
}
