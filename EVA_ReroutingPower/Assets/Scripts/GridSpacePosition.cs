using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpacePosition : MonoBehaviour {

    public GameObject mainCamera;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = mainCamera.transform.position;
    }
}
