using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Numpad : MonoBehaviour
{
    GameObject targetGameObject;

    Component textComponent;
    string output = "";
    //int maxOutputLength = 0;

    const string TEXT = "text";
    const string OK = "ok";
    const string BACK = "del.";


    // Use this for initialization
    void Start()
    {
        textComponent = GetFirstTextComponent(targetGameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetTargetGameObject(GameObject _targetGameObject)
    {
        targetGameObject = _targetGameObject;
    }

    public GameObject GetTargetGameObject()
    {
        return targetGameObject;
    }

    private Component GetFirstTextComponent(GameObject targetGameObject)
    {
        Component[] componentsWithText = targetGameObject.GetComponents<Component>()
            .Where(x => x.GetType().ToString() == "UnityEngine.UI.Text" || x.GetType().ToString() == "UnityEngine.UI.InputField").ToArray();

        return componentsWithText[0];
    }

    /**
     * On Tapping event
     */
    void OnKeyboardTyping(GameObject keyboardItem)
    {
        Text letter = keyboardItem.GetComponentInChildren<Text>();
        string value = letter.text;
        if (value.ToLower().Equals(OK))
        {
            ExitKey();
        }
        else if (value.ToLower().Equals(BACK))
        {
            BackspaceKey();
        }
        else
        {
            TypeKey(value[0]);
        }
    }

    /**
     * Remove keyboard from the screen
     * Remove focus flag in 'targetGameObject'
     * */
    void ExitKey()
    {
        Debug.Log("TypeKey " + OK);
        EventSystem.current.SetSelectedGameObject(null);
        Destroy(gameObject);
    }

    void BackspaceKey()
    {
        Debug.Log("TypeKey " + BACK);
        if (textComponent.GetType().ToString() == "UnityEngine.UI.Text")
        {
            output = ((Text)textComponent).text;
            if (output.Length >= 1)
            {
                ((Text)textComponent).text = output.Remove(output.Length - 1, 1);
            }
        }
        else if (textComponent.GetType().ToString() == "UnityEngine.UI.InputField")
        {
            output = ((InputField)textComponent).text;
            if (output.Length >= 1)
            {
                ((InputField)textComponent).text = output.Remove(output.Length - 1, 1);
            }
        }
    }

    void TypeKey(char key)
    {
        Debug.Log("TypeKey " + key);
        if (textComponent.GetType().ToString() == "UnityEngine.UI.Text")
        {
            output = ((Text)textComponent).text;
            ((Text)textComponent).text = output + key.ToString();

        }
        else if (textComponent.GetType().ToString() == "UnityEngine.UI.InputField")
        {
            output = ((InputField)textComponent).text;
            ((InputField)textComponent).text = output + key.ToString();

        }
    }
}