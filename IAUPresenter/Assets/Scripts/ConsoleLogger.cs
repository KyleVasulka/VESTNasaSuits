using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleLogger : MonoBehaviour
{

    TextMesh textMesh;
    GameObject sound_effect;
    public GameObject worldSpace, evaSphereSpace;
    // Use this for initialization
    void Start()
    {
        textMesh = gameObject.GetComponentInChildren<TextMesh>();
        sound_effect = transform.Find("sound_effect").gameObject;
    }

    void OnEnable()
    {
        Application.logMessageReceived += LogMessage;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= LogMessage;
    }

    public void LogMessage(string message, string stackTrace, LogType type)
    {
        if (textMesh != null && textMesh.text != null)
        {
            if (textMesh.text.Length > 300)
            {
                textMesh.text = message + "\n";
            }
            else
            {
                textMesh.text += message + "\n";
            }
        }
    }

    public void ShowConsoleWindow()
    {
        Debug.Log("Show Logging Window");
        textMesh.color = new Color(1, 1, 1, 1);
        PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
    }

    public void HideConsoleWindow()
    {
        Debug.Log("Hide Logging Window");
        textMesh.color = new Color(1, 1, 1, 0);
        PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
    }
    public void Pin()
    {
        Vector3 position = transform.position;

        transform.SetParent(worldSpace.transform);
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        Debug.Log("Pin Logging Window to position (" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ")");
        float rotate_z = transform.eulerAngles.z;
        transform.Rotate(Vector3.forward, Camera.main.transform.eulerAngles.z - rotate_z);
        PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
    }

    public void UnPin()
    {
        Debug.Log("Unpin Logging Window");
        transform.SetParent(evaSphereSpace.transform);
        transform.localPosition = new Vector3(-1.5f, 0.35f, 1.85f);
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
    }
    private void PlaySoundEffect(string sound_file, bool loop)
    {
        AudioSource audioSource = sound_effect.GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.loop = loop;
            audioSource.clip = Resources.Load(sound_file) as AudioClip;
            audioSource.Play();
        }
    }
}