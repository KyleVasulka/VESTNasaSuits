using UnityEngine;
using UnityEngine.UI;

public class FanErrorSwitch : MonoBehaviour {
    Text value;
    private float nextTime = 0.0f;
    public float period = 1f;


    // Use this for initialization
    void Start () {

       
	}

    // Update is called once per frame
   

	void Update () {

	}
    public void UpdateValue(int contrast_idx)
    {
        value = transform.Find("value_position").transform.GetComponentInChildren<Text>();
        string switchStatus = TelemetryController.suitSwitch.fan_error;
        value.text = TelemetryController.suitSwitch.fan_error;


         if (switchStatus == "1")
        {
          
                transform.GetComponent<Image>().color = Color.red;
        

            
        }
        else
        {

            transform.GetComponent<Image>().color = Color.gray;
        } 
    }
}
   

