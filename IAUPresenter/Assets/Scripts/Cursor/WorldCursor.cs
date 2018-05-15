using UnityEngine;
using UnityEngine.UI;

public class WorldCursor : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private RawImage rawImage;

    // Use this for initialization
    void Start()
    {
        // Grab the mesh renderer that's on the same object as this script.
        //meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
        rawImage = this.gameObject.GetComponentInChildren<RawImage>(); ;
    }

    // Update is called once per frame
    void Update()
    {
        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram...
            // Display the cursor mesh.
            //meshRenderer.enabled = true;
            rawImage.enabled = true;

            // Move thecursor to the point where the raycast hit.
            this.transform.position = hitInfo.point;

            // Rotate the cursor to hug the surface of the hologram.
            this.transform.rotation = Quaternion.FromToRotation(-1 * Vector3.forward, hitInfo.normal);

            //if (hitInfo.transform.localRotation.y > 0.2f)
            //this.transform.Rotate(Vector3.up - rawImage.transform.rotation.ToEuler());
        }
        else
        {
            // If the raycast did not hit a hologram, hide the cursor mesh.
            //meshRenderer.enabled = false;
            rawImage.enabled = false;
        }
    }
}
