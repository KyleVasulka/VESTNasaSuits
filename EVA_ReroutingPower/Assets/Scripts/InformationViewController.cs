using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using System;

public class InformationViewController : MonoBehaviour
{

    public GameObject worldSpace, lifeLine, evaSphereSpace, evaGridSpace;
    public GameObject imagePrefab, videoPrefab, audioPrefab, textPrefab, model3dPrefab;
    public GameObject btnPrefab, iauContentPrefab, loadingScenePrefab;

    private List<FlowEntry> flowEntries = new List<FlowEntry>();
    private IAU currentIau;
    public static AnimationClip[] clips;
    private GameObject iauContent, iauLoadingScene;
    private Transform panel_top, panel_bottom;
    private string TotalStepCount = null;
    GameObject sound_effect;

    public static bool gravity_mode = true;
    int evaId = 74;
    // Use this for initialization
    void Start()
    {
        Resources.LoadAll("");
        AnimationClip[] objs = Resources.FindObjectsOfTypeAll<AnimationClip>();

        clips = objs;

        panel_top = transform.Find("panel_top");
        panel_bottom = transform.Find("panel_bottom");
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        sound_effect = transform.Find("sound_effect").gameObject;



        StartCoroutine(ResetAllIAUsByEvaId(evaId));
        StartCoroutine(UpdateStepCount());
        StartCoroutine(GetIAUByStep(evaId, 1));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator UpdateStepCount()
    {
        string hostName = ConnectionController.HOST_NAME;
        string url = "http://" + hostName + "/NasaSuits/api/eva/iau/GetIAUCount.php?eva_id=" + evaId;
        WWW response = new WWW(url);
        yield return response;
        TotalStepCount = response.text;
    }

    private IEnumerator ResetAllIAUsByEvaId(int evaId)
    {
        string hostName = ConnectionController.HOST_NAME;
        string url = "http://" + hostName + "/NasaSuits/api/eva/iau/UpdateIAU.php?eva_id=" + evaId + "&completed=0&locked=0";
        WWW response = new WWW(url);
        yield return response;
    }


    private IEnumerator GetFlowTableByEvaId(string evaId)
    {
        //create http handler with api for directions table
        string hostName = ConnectionController.HOST_NAME;
        string url = "http://" + hostName + "/NasaSuits/api/eva/GetFlowTable.php?eva_id=" + evaId;
        //Debug.Log(url);
        WWW response = new WWW(url);
        yield return response;
        try
        {
            flowEntries = JsonConvert.DeserializeObject<List<FlowEntry>>(response.text);
            DisplayNextButton(iauContent);
        }
        catch (Exception e)
        {
            Debug.Log("Exception while parsing Json string: " + response.text);
            Debug.Log(e.ToString());
        }
    }

    private IEnumerator GetIAUById(string iauId)
    {
        RemoveAllIAUContent();

        string hostName = ConnectionController.HOST_NAME;
        string url = "http://" + hostName + "/NasaSuits/api/eva/iau/GetIAU.php?iau_id=" + iauId;
        WWW response = new WWW(url);
        yield return response;
        IAU iau = null;
        try
        {
            iau = JsonConvert.DeserializeObject<IAU>(response.text);
        }
        catch (Exception e)
        {
            Debug.Log("Exception while parsing Json object " + response.text);
            Debug.Log(e);
        }

        // If this Iau is not released, display the warning scene
        if (iau != null && !iau.iau_preemption_level.Equals("3"))
            DisplayLoadingScene("This IAU " + iau.iau_step + "." + iau.iau_sub_step + " " + iau.iau_high_level_action + " is not released");

        // while the preemption is not 3, wait until this Iau is released
        WaitForSeconds waitTime = new WaitForSeconds(1);
        while (iau != null && !iau.iau_preemption_level.Equals("3"))
        {
            Debug.Log("IAU is not released");

            response = new WWW(url);
            yield return response;
            try
            {
                iau = JsonConvert.DeserializeObject<IAU>(response.text);
            }
            catch (Exception e)
            {
                Debug.Log("Exception while parsiong Json object " + response.text);
                Debug.Log(e);
            }
            yield return waitTime;
        }


        // Wait until the UpdateStepCount return
        while (TotalStepCount == null)
        {
            yield return waitTime;
        }

        // after the Iau is released
        if (iau != null)
        {
            // When Iau is released, remove the loading scene and show the Iau
            RemoveLoadingScene();
            currentIau = iau;
            DisplayIAU(iau);
            DisplayIUs(iau);

            // load the flow from Flow table related to this Iau
            StartCoroutine(GetFlowTableByEvaId(iau.iau_eva_id));
            // play sound effect to notice the update of IAU view
            PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
        }
    }

    private IEnumerator GetIAUByStep(int evaId, int step)
    {
        RemoveAllIAUContent();
        string hostName = ConnectionController.HOST_NAME;
        string url = "http://" + hostName + "/NasaSuits/api/eva/iau/GetIAU.php?eva_id=" + evaId + "&step=" + step;

        WWW response = new WWW(url);
        yield return response;
        IAU iau = null;
        try
        {
            iau = JsonConvert.DeserializeObject<IAU>(response.text);
        }
        catch (Exception e)
        {
            Debug.Log("Exception while parsing Json object " + response.text);
            Debug.Log(e);
        }

        // If this Iau is not released, display the warning scene
        if (iau != null && !iau.iau_preemption_level.Equals("3"))
            DisplayLoadingScene("This IAU " + iau.iau_step + "." + iau.iau_sub_step + " " + iau.iau_high_level_action + " is not released");

        WaitForSeconds waitTime = new WaitForSeconds(1);
        while (iau != null && !iau.iau_preemption_level.Equals("3"))
        {
            Debug.Log("IAU is not released");
            response = new WWW(url);
            yield return response;
            try
            {
                iau = JsonConvert.DeserializeObject<IAU>(response.text);
            }
            catch (Exception e)
            {
                Debug.Log("Exception while parsiong Json object " + response.text);
                Debug.Log(e);
            }
            yield return waitTime;
        }

        // Wait until the UpdateStepCount return
        while (TotalStepCount == null)
        {
            yield return waitTime;
        }

        if (iau != null)
        {
            // When Iau is released, remove the loading scene and show the Iau
            RemoveLoadingScene();
            currentIau = iau;
            currentIau.iau_locked = "1";
            DisplayIAU(iau);
            DisplayIUs(iau);
            StartCoroutine(UpdateIauLocked(currentIau));
            StartCoroutine(GetFlowTableByEvaId(iau.iau_eva_id));

            // play sound effect to notice the update of IAU view
            PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
        }

    }

    private void DisplayIAU(IAU iau)
    {
        foreach (Transform trans in panel_top.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("iau_content"))
            {
                Destroy(trans.gameObject);
            }
        }

        iauContent = Instantiate(iauContentPrefab);

        iauContent.name = "IAU Content";
        iauContent.tag = "iau_content";
        iauContent.transform.SetParent(panel_top);
        iauContent.transform.localPosition = new Vector3(0, 0, 0);
        iauContent.transform.localRotation = Quaternion.identity;

        string textToDisplay = "Step " + iau.iau_step + "." + iau.iau_sub_step + ": " + iau.iau_high_level_action + "\n"
            + iau.iau_task_of_action;
        if (iau.iau_caution != null && !iau.iau_caution.Trim().Equals(""))
            textToDisplay += "\n" + iau.iau_caution;
        Text[] textComponents = iauContent.transform.GetComponentsInChildren<Text>();
        textComponents[0].text = textToDisplay;
        textComponents[1].text = "Progress: " + iau.iau_step + "/" + TotalStepCount;

        GameObject TotalProgressVisual = iauContent.transform.Find("TotalProgress").gameObject;
        GameObject CurrentProgressVisual = TotalProgressVisual.transform.Find("CurrentProgress").gameObject;
        CurrentProgressVisual.transform.localScale = new Vector3((float)Int32.Parse(iau.iau_step) / (float)Int32.Parse(TotalStepCount), 1, 1);
    }

    private void RemoveAllIAUContent()
    {
        Debug.Log("Remove all IAU contents");
        foreach (Transform trans in panel_top.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("iau_content") || trans.tag.Equals("arc"))
            {
                Destroy(trans.gameObject);
            }
        }
    }

    private void DisplayIUs(IAU iau)
    {
        //destroy current ARControllers
        foreach (Transform trans in panel_top.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc"))
            {
                Destroy(trans.gameObject);
            }
        }
        foreach (Transform trans in panel_bottom.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc"))
            {
                Destroy(trans.gameObject);
            }
        }

        foreach (Transform trans in worldSpace.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc"))
            {
                Destroy(trans.gameObject);
            }
        }

        //create new arcontrollers with new Content
        GameObject obj = null;
        for (int i = 0; i < iau.iau_ius.Count; ++i)
        {
            IU iu = iau.iau_ius[i];
            switch (iu.iu_type)
            {
                case "image":
                    PictureARC pictureARC = new PictureARC();
                    pictureARC.SetLink(iu.iu_link);
                    pictureARC.SetPos(iu.GetPosition());
                    pictureARC.SetScale(iu.GetScale());
                    pictureARC.SetRotation(iu.GetRotation());
                    obj = CreateARC.CreateIU(pictureARC, panel_top, imagePrefab);

                    break;
                case "text":
                    TextARC textARC = new TextARC();
                    textARC.SetLink(iu.iu_link);
                    textARC.SetPos(iu.GetPosition());
                    textARC.SetScale(iu.GetScale());
                    textARC.SetRotation(iu.GetRotation());
                    obj = CreateARC.CreateIU(textARC, panel_top, textPrefab);

                    break;
                case "video":
                    VideoARC videoARC = new VideoARC();
                    videoARC.SetLink(iu.iu_link);
                    videoARC.SetPos(iu.GetPosition());
                    videoARC.SetScale(iu.GetScale());
                    videoARC.SetRotation(iu.GetRotation());
                    obj = CreateARC.CreateIU(videoARC, panel_top, videoPrefab);

                    break;
                case "audio":
                    AudioARC audioARC = new AudioARC();
                    audioARC.SetLink(iu.iu_link);
                    audioARC.SetPos(iu.GetPosition());
                    audioARC.SetScale(iu.GetScale());
                    audioARC.SetRotation(iu.GetRotation());
                    obj = CreateARC.CreateIU(audioARC, panel_top, audioPrefab);

                    break;
                case "3d":
                    Model3dARC model3dARC = new Model3dARC();
                    model3dARC.SetLink(iu.iu_link);
                    model3dARC.SetPos(iu.GetPosition());
                    model3dARC.SetScale(iu.GetScale());
                    model3dARC.SetRotation(iu.GetRotation());
                    CreateARC.CreateIU(model3dARC, panel_bottom, model3dPrefab);

                    break;
                default:
                    Debug.Log("Something went wrong with iu type");
                    break;
            }
        }
    }

    private void DisplayLoadingScene(String textToDisplay)
    {
        Debug.Log("Display Loading Scene");
        iauLoadingScene = GameObject.FindGameObjectWithTag("loading_scene");
        if (iauLoadingScene == null)
        {
            iauLoadingScene = Instantiate(loadingScenePrefab);
            iauLoadingScene.name = "Loading Scene";
            iauLoadingScene.tag = "loading_scene";
            iauLoadingScene.transform.SetParent(panel_top);
            iauLoadingScene.transform.localPosition = new Vector3(0, 0, 0);
            iauLoadingScene.transform.localRotation = Quaternion.identity;
        }
        Text textComponent = iauLoadingScene.transform.GetComponentInChildren<Text>();
        textComponent.text = textToDisplay;
    }

    private void RemoveLoadingScene()
    {
        iauLoadingScene = GameObject.FindGameObjectWithTag("loading_scene");
        if (iauLoadingScene != null)
        {
            Destroy(iauLoadingScene);
        }
    }

    private GameObject GetParent(IU iu)
    {
        GameObject parentSpace = null;
        if (iu.iu_space.Equals("WORLD_SPACE"))
            parentSpace = worldSpace;
        else if (iu.iu_space.Equals("LIFE_LINE"))
            parentSpace = lifeLine;
        else if (iu.iu_space.Equals("EVA_SPHERE_SPACE"))
            parentSpace = evaSphereSpace;
        else if (iu.iu_space.Equals("EVA_GRID_SPACE"))
            parentSpace = evaGridSpace;
        return parentSpace;
    }

    public void DisplayNextButton(GameObject parent)
    {
        foreach (Transform trans in panel_top.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("confirm_button"))
            {
                Destroy(trans.gameObject);
            }
        }

        GameObject nextButton = Instantiate(btnPrefab, new Vector3(0, -2, 0), Quaternion.identity);
        nextButton.name = "Confirm Button";
        nextButton.tag = "confirm_button";
        nextButton.transform.SetParent(parent.transform);
        nextButton.transform.localPosition = new Vector3(0, -45f, 0);

        nextButton.transform.localScale = new Vector3(1, 1, 1);
        nextButton.transform.localRotation = Quaternion.identity;
        Button btn = nextButton.GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(OnCompleteIau);
        }
    }

    public void OnCompleteIau()
    {
        if (currentIau.iau_confirm_level.Equals("3"))
        {
            currentIau.iau_completed = "1";
            StartCoroutine(UpdateIauStatus(currentIau));
        }
        else
        {
            currentIau.iau_completed = "-1";
            StartCoroutine(UpdateIauStatus(currentIau));

        }
        StartCoroutine(WaitForCompletionAndGoForward(Int32.Parse(currentIau.iau_id)));
        PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
    }

    private IEnumerator UpdateIauStatus(IAU iau)
    {
        string hostName = ConnectionController.HOST_NAME;
        string url = "http://" + hostName + "/NasaSuits/api/eva/iau/UpdateIAU.php?iau_id=" + iau.iau_id + "&completed=" + iau.iau_completed;
        WWW response = new WWW(url);
        yield return response;
        Debug.Log("Update completion status for " + iau.iau_step + "." + iau.iau_sub_step + ": " + iau.iau_high_level_action + ", set completed=" + iau.iau_completed + ". " + response.text);
    }

    private IEnumerator UpdateIauLocked(IAU iau)
    {
        string hostName = ConnectionController.HOST_NAME;
        string url = "http://" + hostName + "/NasaSuits/api/eva/iau/UpdateIAU.php?iau_id=" + iau.iau_id + "&locked=" + iau.iau_locked;
        WWW response = new WWW(url);
        yield return response;
        Debug.Log("Update locked for " + iau.iau_step + "." + iau.iau_sub_step + ": " + iau.iau_high_level_action + ", set locked=" + iau.iau_locked + ". " + response.text);
    }
    private IEnumerator UpdateIauLocked(string iauId, string locked)
    {
        string hostName = ConnectionController.HOST_NAME;
        string url = "http://" + hostName + "/NasaSuits/api/eva/iau/UpdateIAU.php?iau_id=" + iauId + "&locked=" + locked;
        WWW response = new WWW(url);
        yield return response;
        Debug.Log("Update locked for IAU with id=" + iauId + ", set locked=" + locked + ". " + response.text);
    }


    private IEnumerator WaitForCompletionAndGoForward(int iauId)
    {
        if (!currentIau.iau_confirm_level.Equals("3"))
        {
            string hostName = ConnectionController.HOST_NAME;
            string url = "http://" + hostName + "/NasaSuits/api/eva/iau/GetIAU.php?iau_id=" + iauId;
            WWW response = new WWW(url);
            yield return response;
            IAU iau = null;
            try
            {
                iau = JsonConvert.DeserializeObject<IAU>(response.text);
            }
            catch (Exception e)
            {
                Debug.Log("Exception while parsiong Json object " + response.text);
                Debug.Log(e);
            }

            if (iau != null && !iau.iau_completed.Equals("1"))
            {
                RemoveAllIAUContent();
                DisplayLoadingScene("IAU " + currentIau.iau_step + "." + currentIau.iau_sub_step + ": " + currentIau.iau_high_level_action + " is checking by Flight Crews and Groung Crews.\nWaiting for Completion status");
            }

            // wait until the IAU is marked completed by Flight Crews or Ground Crews
            WaitForSeconds waitTime = new WaitForSeconds(1);
            while (iau != null && !iau.iau_completed.Equals("1"))
            {
                Debug.Log("IAU is waiting for Completion");
                response = new WWW(url);
                yield return response;
                try
                {
                    iau = JsonConvert.DeserializeObject<IAU>(response.text);
                }
                catch (Exception e)
                {
                    Debug.Log("Exception while parsiong Json object " + response.text);
                    Debug.Log(e);
                }
                yield return waitTime;
            }

            // When the IAU is marked completed
            // Remove the loading scene
            RemoveLoadingScene();
        }

        // release the lock of the current IAU
        currentIau.iau_locked = "0";
        StartCoroutine(UpdateIauLocked(currentIau));

        // Go to the next IAU base on the flow table
        for (int i = 0; i < flowEntries.Count; i++)
        {
            FlowEntry flow = flowEntries[i];
            if (flow.current_iau_id.Equals(currentIau.iau_id))
            {
                if (flow.future_iau_id != null)
                {
                    StartCoroutine(GetIAUById(flow.future_iau_id));
                    // lock the future Iau
                    StartCoroutine(UpdateIauLocked(flow.future_iau_id, "1"));

                    // play sound effect noticing the Astronaut of the update
                    PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
                }
                else
                {
                    GameObject btn = GameObject.FindGameObjectWithTag("confirm_button");
                    if (btn != null)
                    {
                        btn.GetComponent<Button>().interactable = false;
                        btn.GetComponentInChildren<Text>().text = "End";
                    }
                }
                break;
            }
        }

    }

    private void GoForwardToNextStep(int iauId)
    {
        // release the lock of the current IAU
        currentIau.iau_locked = "0";
        StartCoroutine(UpdateIauLocked(currentIau));

        // Go to the next IAU base on the flow table
        for (int i = 0; i < flowEntries.Count; i++)
        {
            FlowEntry flow = flowEntries[i];
            if (flow.current_iau_id.Equals(currentIau.iau_id))
            {
                if (flow.future_iau_id != null)
                {
                    StartCoroutine(GetIAUById(flow.future_iau_id));
                    // lock the future Iau
                    StartCoroutine(UpdateIauLocked(flow.future_iau_id, "1"));

                    // play sound effect noticing the Astronaut of the update
                    PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
                }
                else
                {
                    GameObject btn = GameObject.FindGameObjectWithTag("confirm_button");
                    if (btn != null)
                    {
                        btn.GetComponent<Button>().interactable = false;
                        btn.GetComponentInChildren<Text>().text = "End";
                    }
                }
                break;
            }
        }

    }

    public void CenterView()
    {
        transform.SetParent(evaSphereSpace.transform);
        transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
        Debug.Log("Move the main view to center position");
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);

        //float rotate_z = transform.eulerAngles.z;
        //transform.Rotate(Vector3.forward, Camera.main.transform.eulerAngles.z - rotate_z);
        GameObject heldView = GameObject.FindGameObjectWithTag("held_iau_view");
        if (heldView != null)
        {
            heldView.transform.position = transform.position + transform.up * 0.65f;
        }

        PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
    }

    public void PlayVideo()
    {
        foreach (Transform trans in panel_top.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("VideoARC"))
            {
                ApplyVideo applyVideo = trans.GetComponent<ApplyVideo>();
                if (applyVideo != null)
                {
                    applyVideo.Play();
                    PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
                }
            }
        }

    }

    public void PauseVideo()
    {
        foreach (Transform trans in panel_top.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("VideoARC"))
            {
                ApplyVideo applyVideo = trans.GetComponent<ApplyVideo>();
                if (applyVideo != null)
                {
                    applyVideo.Pause();
                    PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
                }
            }
        }

    }

    public void PlayAudio()
    {
        foreach (Transform trans in panel_top.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("AudioARC"))
            {
                ApplyAudio applyAudio = trans.GetComponent<ApplyAudio>();
                if (applyAudio != null)
                {
                    applyAudio.Play();
                    PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
                }
            }
        }

    }

    public void PauseAudio()
    {
        foreach (Transform trans in panel_top.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("AudioARC"))
            {
                ApplyAudio applyAudio = trans.GetComponent<ApplyAudio>();
                if (applyAudio != null)
                {
                    applyAudio.Pause();
                    PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
                }
            }
        }

    }

    public void LoadAction(int step)
    {
        StartCoroutine(GetIAUByStep(evaId, step));
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

    public void RotateImage()
    {
        foreach (Transform trans in panel_top.GetComponentsInChildren<Transform>())
        {

            if (trans.tag.Equals("arc") && trans.name.Equals("PictureARC"))
            {
                trans.Rotate(0, 0, -90);
            }
        }
        PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
    }

    public void RotateText()
    {
        foreach (Transform trans in panel_top.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("iau_content"))
            {
                trans.Rotate(0, 0, -90);
                PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
            }
        }
    }

    public void RotateVideo()
    {
        foreach (Transform trans in panel_top.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("VideoARC"))
            {
                trans.Rotate(0, 0, -90);
                PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
            }
        }
    }

    public void RotateModelX()
    {
        foreach (Transform trans in panel_bottom.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("3dARC"))
            {
                trans.Rotate(-90, 0, 0);
                PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
            }
        }
        foreach (Transform trans in worldSpace.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("3dARC"))
            {
                trans.Rotate(-90, 0, 0);
                PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
            }
        }
    }

    public void RotateModelY()
    {
        foreach (Transform trans in panel_bottom.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("3dARC"))
            {
                trans.Rotate(0, -90, 0);
                PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
            }
        }
        foreach (Transform trans in worldSpace.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("3dARC"))
            {
                trans.Rotate(0, -90, 0);
                PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
            }
        }
    }

    public void RotateModelZ()
    {
        foreach (Transform trans in panel_bottom.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("3dARC"))
            {
                trans.Rotate(0, 0, -90);
                PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
            }
        }
        foreach (Transform trans in worldSpace.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("3dARC"))
            {
                trans.Rotate(0, 0, -90);
                PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
            }
        }
    }

    Vector3 model_3d_scale = new Vector3(1, 1, 1);
    public void Show3dModel()
    {
        foreach (Transform trans in panel_bottom.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("3dARC"))
            {
                trans.localScale = model_3d_scale;
                PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
            }
        }
        foreach (Transform trans in worldSpace.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("3dARC"))
            {
                trans.localScale = model_3d_scale;
                PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
            }
        }
    }

    public void Hide3dModel()
    {
        foreach (Transform trans in panel_bottom.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("3dARC"))
            {
                model_3d_scale = trans.localScale;
                trans.localScale = new Vector3(0, 0, 0);
                PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
            }
        }
        foreach (Transform trans in worldSpace.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("3dARC"))
            {
                model_3d_scale = trans.localScale;
                trans.localScale = new Vector3(0, 0, 0);
                PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
            }
        }
    }

    public void PinModel()
    {
        foreach (Transform trans in panel_bottom.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("3dARC"))
            {
                trans.SetParent(worldSpace.transform);
                trans.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;
                Debug.Log("Pin Model to position (" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ")");
                PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
            }
        }
        foreach (Transform trans in worldSpace.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("3dARC"))
            {
                trans.SetParent(worldSpace.transform);
                trans.position = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;
                Debug.Log("Pin Model to position (" + transform.position.x + ", " + transform.position.y + ", " + transform.position.z + ")");
                PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
            }
        }
    }

    public void UnPinModel()
    {
        foreach (Transform trans in worldSpace.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("3dARC"))
            {
                trans.SetParent(panel_bottom);
                trans.localPosition = new Vector3(0, 0, 0);
                Debug.Log("UnPin Model");
                PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
            }
        }
    }

    public void HoldView()
    {
        // clear the previous view if exist
        GameObject[] objs = GameObject.FindGameObjectsWithTag("held_iau_view");
        foreach (GameObject obj in objs)
        {
            Destroy(obj);
        }

        // Instantiate a new view
        GameObject heldView = Instantiate(gameObject);
        Destroy(heldView.transform.GetComponent<InformationViewController>());

        heldView.name = "InformationSubView";
        heldView.tag = "held_iau_view";
        heldView.transform.SetParent(evaSphereSpace.transform);
        heldView.transform.position = transform.position + transform.up * 0.625f;
        //heldView.transform.rotation = Quaternion.LookRotation(heldView.transform.position - Camera.main.transform.position);

        Transform sub_panel_top = heldView.transform.Find("panel_top");
        Transform sub_panel_bottom = heldView.transform.Find("panel_bottom");

        // remove Next button of the held view
        foreach (Transform trans in sub_panel_top.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("confirm_button"))
            {
                Destroy(trans.gameObject);
            }
        }

        // remove 3d model of the held view
        foreach (Transform trans in sub_panel_bottom.GetComponentsInChildren<Transform>())
        {
            if (trans.tag.Equals("arc") && trans.name.Equals("3dARC"))
            {
                Destroy(trans.gameObject);
            }
        }

        // copy video url
        Transform[] sub_trans_arr = sub_panel_top.GetComponentsInChildren<Transform>();
        Transform[] main_trans_arr = panel_top.GetComponentsInChildren<Transform>();
        for (int i = 0; i < sub_trans_arr.Length; i++)
        {
            if (sub_trans_arr[i].tag.Equals("arc") && sub_trans_arr[i].name.Equals("VideoARC") && main_trans_arr[i].tag.Equals("arc") && main_trans_arr[i].name.Equals("VideoARC"))
            {
                sub_trans_arr[i].GetComponent<ApplyVideo>().link = main_trans_arr[i].GetComponent<ApplyVideo>().link;
            }
            if (sub_trans_arr[i].tag.Equals("arc") && sub_trans_arr[i].name.Equals("AudioARC") && main_trans_arr[i].tag.Equals("arc") && main_trans_arr[i].name.Equals("AudioARC"))
            {
                sub_trans_arr[i].GetComponent<ApplyAudio>().link = main_trans_arr[i].GetComponent<ApplyAudio>().link;
            }
        }
        PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
    }

    public void ReleaseView()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("held_iau_view");
        foreach (GameObject obj in objs)
        {
            Destroy(obj);
        }

        PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
    }

    public void GravityOn()
    {
        Debug.Log("Gravity mode turned on");
        gravity_mode = true;
        PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
    }

    public void GravityOff()
    {
        Debug.Log("Gravity mode turned off");
        gravity_mode = false;
        PlaySoundEffect(TelemetryController.SOUND_EFFECT_COMPLETE, false);
    }
}
