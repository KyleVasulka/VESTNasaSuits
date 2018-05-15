using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ApplyAudio : MonoBehaviour
{

    public AudioClip SoundClip;
    public GameObject playIcon;
    AudioSource audioSource;
    public string link;
    bool firstRun = true;
    bool isPaused = false;

    public void SetLink(string newlink)
    {
        link = newlink;
    }

    // Use this for initialization
    void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
        StartCoroutine(GetAudioClip());
    }

    public void PlayPauseAudio()
    {
        Debug.Log("Audio lay button pressed");
        if (!firstRun && !isPaused)
        {
            Debug.Log("Condition 1");
            //if (audioSource.isPlaying)
            //{
                audioSource.Pause();
                playIcon.GetComponent<RawImage>().texture = Resources.Load("Speaker_Icon_off") as Texture2D;
                isPaused = true;
            //}
        }
        else if (!firstRun && isPaused)
        {
            Debug.Log("Condition 2");
            //if (!audioSource.isPlaying && (audioSource.clip.loadState == AudioDataLoadState.Loaded)) 
            //{
                audioSource.Play();
                playIcon.GetComponent<RawImage>().texture = Resources.Load("Speaker_Icon") as Texture2D;
                isPaused = false;
            //}
        }
        else
        {
            Debug.Log("Condition 3");
            
            StartCoroutine(PrepareAndPlayAudio());
        }
    }

    IEnumerator GetAudioClip()
    {
        WWW www = new WWW(link);
        yield return www;
        audioSource.clip = www.GetAudioClip();
    }

    IEnumerator PrepareAndPlayAudio()
    {
        WaitForSeconds waitTime = new WaitForSeconds(1);
        while (audioSource.clip == null)
        {
            Debug.Log("Preparing Audio");
            yield return waitTime;
        }
        Debug.Log("Done Preparing Audio");
       
        //if (!audioSource.isPlaying && (audioSource.clip.loadState == AudioDataLoadState.Loaded))
        //{
            Debug.Log("Playing Audio");
            playIcon.GetComponent<RawImage>().texture = Resources.Load("Speaker_Icon") as Texture2D;
            audioSource.Play();
            firstRun = false;
        //}
    }


    // Update is called once per frame
    void Update()
    {
        //can change to different key to activate sound
        if (Input.GetKeyDown("space"))
        {
            PlayPauseAudio();
        }
    }

    public void Play()
    {
        Debug.Log("Play Audio");
        if (!firstRun)
        {
            //if (!audioSource.isPlaying && (audioSource.clip.loadState == AudioDataLoadState.Loaded))
            //{
                audioSource.Play();
                playIcon.GetComponent<RawImage>().texture = Resources.Load("Speaker_Icon") as Texture2D;
                isPaused = false;
            //}
        }
        else
        {
            //StartCoroutine(GetAudioClip());
            StartCoroutine(PrepareAndPlayAudio());
        }
    }

    public void Pause()
    {
        Debug.Log("Pause Audio");
        if (!firstRun && !isPaused)
        {
            //if (audioSource.isPlaying)
            //{
                audioSource.Pause();
                playIcon.GetComponent<RawImage>().texture = Resources.Load("Speaker_Icon_off") as Texture2D;
                isPaused = true;
            //}
        }
    }
}
