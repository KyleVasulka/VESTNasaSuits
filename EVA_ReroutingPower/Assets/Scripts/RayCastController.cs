using UnityEngine;
using UnityEngine.XR.WSA.Input;
public class RayCastController : MonoBehaviour
{
    private GameObject oldFocusedObject, currentFocusedObject;
    private GestureRecognizer recognizer;

    void Awake()
    {
        recognizer = new GestureRecognizer();
        recognizer.SetRecognizableGestures(GestureSettings.Tap);
        recognizer.TappedEvent += Recognizer_TappedEvent;
        recognizer.StartCapturingGestures();
    }

    void Start()
    {
        // Grab the mesh renderer that's on the same object as this script.
        //meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
    }

    void Update()
    {
        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            //sets focused object to the object that was hit
            currentFocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            currentFocusedObject = null;
        }

        //tests to see if a different object was hit, this means that the old object should run GazeExit because 
        //it is no longer being hovered over, and the current object should run GazeEnter because it has started 
        //being hovered over. If one is null, than code shouldn't run
        if (oldFocusedObject != currentFocusedObject)
        {
            if (oldFocusedObject != null)
            {
                oldFocusedObject.SendMessage("GazeExit", SendMessageOptions.DontRequireReceiver);
            }

            if (currentFocusedObject != null)
            {
                currentFocusedObject.SendMessage("GazeEnter", SendMessageOptions.DontRequireReceiver);
            }

        }

        oldFocusedObject = currentFocusedObject;
    }

    public void Recognizer_TappedEvent(InteractionSourceKind source, int tapCount, Ray headRay)
    {
        if (currentFocusedObject != null)
        {
             if (currentFocusedObject.tag == "keyboard_item")
            {
                currentFocusedObject.SendMessage("OnKeyboardItemSelection");
            }
        }
        currentFocusedObject = null;
    }
}
