using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumpadItem : MonoBehaviour
{
    Material keyNormalMaterial;
    Material keySelectedMaterial;
    const string QUAD_FRONT = "Front";

    /* Getters and Setters */
    public void SetKeyNormalMaterial(Material keyNormalMaterial)
    {
        //Debug.Log("Set Key Normal material");
        this.keyNormalMaterial = keyNormalMaterial;
        transform.Find(QUAD_FRONT).GetComponent<Renderer>().material = keyNormalMaterial;
    }

    public void SetKeySelectedMaterial(Material keySelectedMaterial)
    {
        //Debug.Log("Set Key Selected material");
        this.keySelectedMaterial = keySelectedMaterial;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void GazeEnter()
    {
        Debug.Log("GazeEnter on " + name);
        transform.Find(QUAD_FRONT).GetComponent<Renderer>().material = keySelectedMaterial;
    }

    void GazeExit()
    {
        Debug.Log("GazeExit on " + name);
        transform.Find(QUAD_FRONT).GetComponent<Renderer>().material = keyNormalMaterial;
    }

    void OnKeyboardItemSelection()
    {
        Debug.Log("Tap on key " + name);
        transform.parent.SendMessage("OnKeyboardTyping", gameObject);
    }
}