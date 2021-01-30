using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RadioPrompt : MonoBehaviour
{
    public TMP_Text radioPromptText;
    public TMP_InputField inputField;
    public float timeBetweenLetters;

    const string message = "What should I say?\n";

    RadioController radioController;
    bool promptEnabled = false;

    void Start()
    {
        radioController = GetComponent<RadioController>();
    }

    void Update()
    {
        if(promptEnabled && Input.GetKeyDown(KeyCode.Return))
        {
            promptEnabled = false;
            DisablePrompt();
        }
    }

    IEnumerator TypeWrite()
    {
        foreach(char c in message)
        {
            radioPromptText.text += c;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
    }

    public void EnablePrompt()
    {
        promptEnabled = true;
        StartCoroutine(TypeWrite());
        inputField.gameObject.SetActive(true);
        inputField.ActivateInputField();
    }

    public void DisablePrompt()
    {
        inputField.DeactivateInputField();
        radioController.ResetClicker();
        //Reach out to Matas' game state system with message
        //inputField.text
        inputField.text = "";
        radioPromptText.text = "";
        inputField.gameObject.SetActive(false);
    }
}
