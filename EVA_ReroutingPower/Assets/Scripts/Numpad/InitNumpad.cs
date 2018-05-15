using UnityEngine;
using UnityEngine.UI;

public class InitNumpad : MonoBehaviour
{

    public GameObject numpadPrefab;

    public Material keyNormalMaterial;
    public Material keySelectedMaterial;

    InputField m_InputField;
    GameObject numpad;
    GameObject[] keyboards;


    void Start()
    {
        m_InputField = GetComponent<InputField>();
    }

    void Update()
    {
        //Check if the Input Field is in focus and able to alter
        if (m_InputField.isFocused)
        {
            keyboards = GameObject.FindGameObjectsWithTag("keyboard");
            if (keyboards.Length == 0)
            {
                InitiateNumpad();
            }
            else
            {
                for (int i = 0; i < keyboards.Length; i++)
                {
                    Numpad numpadScript = keyboards[i].GetComponent<Numpad>() as Numpad;
                    if (numpadScript.GetTargetGameObject() != gameObject)
                        Destroy(keyboards[i]);
                }
            }
        }
    }

    void InitiateNumpad()
    {
        Vector3 position = transform.position;
        Vector3 localPosition = transform.localPosition;

        numpad = Instantiate(numpadPrefab, position, Quaternion.identity);
        numpad.transform.SetParent(transform.parent);

        /* Positioning the keyboard on the screen */
        numpad.transform.localScale = new Vector3(5, 5, 5);
        numpad.transform.localPosition = new Vector3(0, localPosition.y, localPosition.z - 0.1f);
        numpad.transform.localRotation = Quaternion.identity;
        numpad.name = "Numpad";
        numpad.tag = "keyboard";

        /* Adding Numpad C# script to 'numpad' */
        Numpad numpadScript = numpad.AddComponent<Numpad>(); ;
        numpadScript.SetTargetGameObject(gameObject);

        /* Adding NumpadItem C# script to every child gameobject of 'numpad' */
        int childCount = numpad.transform.childCount;
        GameObject numpadKey;
        for (int i = 0; i < childCount; i++)
        {
            numpadKey = numpad.transform.GetChild(i).gameObject;
            numpadKey.tag = "keyboard_item";
            NumpadItem numpadItemScript = numpadKey.AddComponent<NumpadItem>();
            numpadItemScript.SetKeyNormalMaterial(keyNormalMaterial);
            numpadItemScript.SetKeySelectedMaterial(keySelectedMaterial);
        }
    }
}