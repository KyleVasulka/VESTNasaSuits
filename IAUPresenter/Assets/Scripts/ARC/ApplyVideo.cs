using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ApplyVideo : MonoBehaviour
{

    public RawImage image;
    public GameObject playIcon;
    public VideoClip videoToPlay;
    private VideoPlayer videoPlayer;
    private AudioSource audioSource;

    private bool isPaused = false;
    private bool firstRun = true;
    public string link;
    public void SetLink(string newlink)
    {
        link = newlink;
        //StartCoroutine(PlayVideo());
    }

    // Use this for initialization
    void Start()
    {
        Application.runInBackground = true;
    }

    IEnumerator PlayVideo()
    {
        playIcon.SetActive(false);
        firstRun = false;

        //Add VideoPlayer to the GameObject
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        //Add AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        // Vide clip from Url
        videoPlayer.source = VideoSource.Url;
        //Debug.Log(videoPlayer.url);
        videoPlayer.url = link;


        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.controlledAudioTrackCount = 1;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
        videoPlayer.Prepare();


        //Wait until video is prepared
        //Check if videoPlayer is prepared for each 1 second
        WaitForSeconds waitTime = new WaitForSeconds(1);
        while (!videoPlayer.isPrepared)
        {
            Debug.Log("Preparing Video");
            yield return waitTime;
        }

        Debug.Log("Done Preparing Video");

        //Assign the Texture from Video to RawImage to be displayed
        image.texture = videoPlayer.texture;

        //Play Video
        videoPlayer.Play();

        //Play Sound
        audioSource.Play();

        Debug.Log("Playing Video");
        while (videoPlayer.isPlaying)
        {
            //Debug.Log("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }
        Debug.Log("Done Playing Video");
    }

    public void PlayPause()
    {
        Debug.Log("video play button pressed");
        if (!firstRun && !isPaused)
        {
            videoPlayer.Pause();
            audioSource.Pause();
            playIcon.SetActive(true);
            isPaused = true;

        }
        else if (!firstRun && isPaused)
        {
            videoPlayer.Play();
            audioSource.Play();
            playIcon.SetActive(false);
            isPaused = false;
        }
        else
        {
            StartCoroutine(PlayVideo());
        }
    }

    public void Play()
    {
        if (!firstRun)
        {
            Debug.Log("Play Video");
            videoPlayer.Play();
            audioSource.Play();
            playIcon.SetActive(false);
            isPaused = false;
        }
        else
        {
            Debug.Log("Play Video");
            StartCoroutine(PlayVideo());
        }
    }

    public void Pause()
    {
        Debug.Log("Pause Video");
        if (!firstRun && !isPaused)
        {
            videoPlayer.Pause();
            audioSource.Pause();
            playIcon.SetActive(true);
            isPaused = true;

        }
    }
}
