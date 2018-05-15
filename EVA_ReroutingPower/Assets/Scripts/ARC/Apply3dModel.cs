using System.Collections;
using UnityEngine;

public class Apply3dModel : MonoBehaviour
{
    public void ApplyModel(string url, Transform parent)
    {
        StartCoroutine(DownloadAndInstantiateAssetBundle(url, parent));
    }

    IEnumerator DownloadAndInstantiateAssetBundle(string url, Transform parent)
    {
        WWW request = new WWW(url);
        yield return request;
        if (request.error == null)
        {
            Debug.Log("Load asset bundle file " + request.assetBundle.name);
            AssetBundle assetBundle = request.assetBundle;

            string[] asset_names = assetBundle.GetAllAssetNames();
            foreach (string asset_name in asset_names)
            {
                GameObject asset = (GameObject)assetBundle.LoadAsset(asset_name);
                GameObject _gameObject = Instantiate(asset);
                //_gameObject.name = asset_name;
                _gameObject.transform.SetParent(parent);
                _gameObject.transform.localPosition = new Vector3(0, 0, 0);
                _gameObject.transform.localRotation = Quaternion.identity;
                // _gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
            assetBundle.Unload(false);
        }
        else
        {
            Debug.Log(request.error);
        }
    }

    IEnumerator PlayAnimation(GameObject _gameObject, AnimationClip clip)
    {
        Animation animation = _gameObject.AddComponent<Animation>();
        WaitForSeconds waitTime = new WaitForSeconds(1);
        while (animation == null || !animation.isActiveAndEnabled)
        {
            Debug.Log("Preparing Animation");
            yield return waitTime;
        }

        animation.AddClip(clip, clip.name);
        animation.Play(clip.name);

    }

    public void PlayPause(GameObject _gameObject, AnimationClip clip)
    {
        StartCoroutine(PlayAnimation(_gameObject, clip));
    }
}
