using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class URLTextLoader : MonoBehaviour {
    String link;
    //this script only runs when the link is an http request. otherwise the texture is loaded through CreateARC.cs
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Link is set through createARC.cs and then this sets the image through www
    public void setLink(string newLink)
    {
        link = newLink;
        StartCoroutine(LoadText());
    }

    IEnumerator LoadText()
    {
        var www = new WWW(link);

        yield return www;
        if (String.IsNullOrEmpty(www.error))
        {
            yield return "success";
            this.GetComponent<Text>().text = www.text;
        }
        else
        {
            yield return "fail";
            Debug.Log("Text failed to load from " + link);
        }
    }
}
