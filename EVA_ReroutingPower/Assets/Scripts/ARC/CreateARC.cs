using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class CreateARC : MonoBehaviour
{
    static GameObject _gameObject;
    // Use this for initialization
    void Start()
    {//called IAU LOAD
    }


    // Update is called once per frame
    void Update()
    {

    }


    // precondition: arc.position and arc.link must be filled
    public static GameObject CreateIU(TextARC arc, Transform parentSpace, GameObject prefab)
    {
        _gameObject = Instantiate(prefab, new Vector3(), Quaternion.identity);
        _gameObject.name = "TextARC";
        _gameObject.tag = "arc";
        _gameObject.transform.SetParent(parentSpace.transform);
        _gameObject.transform.localPosition = arc.GetPos();
        _gameObject.transform.localRotation = arc.GetRotation();
        _gameObject.transform.localScale = arc.GetScale();


        if (arc.GetLink().Substring(0, 4) == "http")
        {
            _gameObject.transform.Find("Text").GetComponent<URLTextLoader>().setLink(arc.GetLink());
        }
        else//text is a local file
        {
            Text textComponent = _gameObject.transform.Find("Text").GetComponent<Text>();
            textComponent.text = File.ReadAllText(arc.GetLink());
        }
        return _gameObject;
    }

    public static GameObject CreateIU(PictureARC arc, Transform parentSpace, GameObject prefab)
    {
        _gameObject = Instantiate(prefab, new Vector3(), Quaternion.identity);
        _gameObject.name = "PictureARC";
        _gameObject.tag = "arc";
        _gameObject.transform.SetParent(parentSpace.transform);
        _gameObject.transform.localPosition = arc.GetPos();
        _gameObject.transform.localRotation = arc.GetRotation();
        _gameObject.transform.localScale = arc.GetScale();

        if (arc.GetLink().Substring(0, 4) == "http")//image loaded through http
        {
            _gameObject.transform.Find("RawImage").GetComponent<URLImageLoader>().SetLink(arc.GetLink());
        }
        else//image loaded localy
        {
            Texture2D texture2D = new Texture2D(2, 2);
            texture2D.LoadImage(File.ReadAllBytes(arc.GetLink()));
            _gameObject.transform.Find("RawImage").GetComponent<RawImage>().texture = texture2D;
        }
        return _gameObject;

    }

    public static GameObject CreateIU(VideoARC arc, Transform parentSpace, GameObject prefab)
    {
        _gameObject = Instantiate(prefab, new Vector3(), Quaternion.identity);
        _gameObject.name = "VideoARC";
        _gameObject.tag = "arc";
        _gameObject.transform.SetParent(parentSpace.transform);
        _gameObject.transform.localPosition = arc.GetPos();
        _gameObject.transform.localRotation = arc.GetRotation();
        _gameObject.transform.localScale = arc.GetScale();

        //set link
        _gameObject.GetComponent<ApplyVideo>().SetLink(arc.GetLink());
        return _gameObject;
    }

    public static GameObject CreateIU(AudioARC arc, Transform parentSpace, GameObject prefab)
    {
        _gameObject = Instantiate(prefab, new Vector3(), Quaternion.identity);
        _gameObject.name = "AudioARC";
        _gameObject.tag = "arc";
        _gameObject.transform.SetParent(parentSpace.transform);
        _gameObject.transform.localPosition = arc.GetPos();
        _gameObject.transform.localRotation = arc.GetRotation();
        _gameObject.transform.localScale = arc.GetScale();

        //set link
        _gameObject.GetComponent<ApplyAudio>().SetLink(arc.GetLink());
        return _gameObject;
    }

    public static GameObject CreateIU(Model3dARC arc, Transform parentSpace, GameObject prefab)
    {
        _gameObject = Instantiate(prefab, new Vector3(), Quaternion.identity);
        _gameObject.name = "3dARC";
        _gameObject.tag = "arc";
        _gameObject.transform.SetParent(parentSpace.transform);
        _gameObject.transform.localPosition = arc.GetPos();
        _gameObject.transform.localRotation = arc.GetRotation();
        _gameObject.transform.localScale = arc.GetScale();

        _gameObject.GetComponent<Apply3dModel>().ApplyModel(arc.GetLink(), _gameObject.transform);
        return _gameObject;
    }
}
