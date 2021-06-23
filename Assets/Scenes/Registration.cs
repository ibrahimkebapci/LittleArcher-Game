using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class Registration : MonoBehaviour
{
    public InputField nameField;
    public InputField passordField;

    public Button sumbitButton;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }
    IEnumerator Register()
    {
        WWWForm form= new WWWForm();
        form.AddField("name", nameField.text);
        form.AddField("password", passordField.text);
        WWW www = new WWW("http://localhost/sqlconnect/register.php",form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("User created successfully");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        else
        {
            Debug.Log("user creation failed. Error #" + www.text);
        }
    }
    public void VerifyInputs()
    {
        sumbitButton.interactable = (nameField.text.Length >= 1 && passordField.text.Length >= 1);
    }
}