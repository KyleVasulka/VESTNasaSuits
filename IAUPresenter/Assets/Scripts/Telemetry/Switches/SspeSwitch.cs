using UnityEngine;
using UnityEngine.UI;


public class SspeSwitch : MonoBehaviour {
    Text value;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void UpdateValue(int contrast_idx)
    {
        value = transform.Find("value_position").transform.GetComponentInChildren<Text>();
        string switchStatus = TelemetryController.suitSwitch.sspe;
        value.text = TelemetryController.suitSwitch.sspe;
        if (switchStatus == "1")
        {
            transform.GetComponent<Image>().color = Color.red;
        }
        else
        {
            transform.GetComponent<Image>().color = Color.grey;
        }
    }
}
