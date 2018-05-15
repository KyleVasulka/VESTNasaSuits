using System.Collections;
using UnityEngine;

public class Apply3dModel : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
        StartCoroutine(PlayAnimation(_gameObject,clip));
    }
}
