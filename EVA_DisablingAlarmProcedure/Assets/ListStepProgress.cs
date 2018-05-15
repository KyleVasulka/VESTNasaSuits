using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListStepProgress : MonoBehaviour {
    public GameObject textGO;
    private UnityEngine.UI.Text text;
   
	// Use this for initialization
	void Start () {
        text = textGO.GetComponent<UnityEngine.UI.Text>() ;
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void UpdateText(string thisStepNumber, string TotalStepNumber)
    {
        text.text = "Progress: " + thisStepNumber + "/" + TotalStepNumber;
    }
}
