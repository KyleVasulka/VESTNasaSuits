using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class URLImageLoader : MonoBehaviour {
    String link;
    //this script only runs when the link is an http request. otherwise the texture is loaded through CreateARC.cs
    // Use this for initialization
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Link is set through createARC.cs and then this sets the image through www
    public void SetLink(string newLink)
    {
        link = newLink;
        StartCoroutine(LoadImage());
    }

    IEnumerator LoadImage()
    {
        var www = new WWW(link);

        yield return www;
        if (String.IsNullOrEmpty(www.error))
        {
            yield return "success";
            Texture2D textur = new Texture2D(2, 2);
            www.LoadImageIntoTexture(textur);
            this.GetComponent<RawImage>().texture = textur;
        }
        else
        {
            yield return "fail";
            Debug.Log("Image failed to load from" + link);
        }
    }
}
