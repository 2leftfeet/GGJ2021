using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MonologueSystem : MonoBehaviour
{
    public static MonologueSystem instance;

    public TMP_Text textElement;
    public float timeBetweenLetters;
    public float fadeTime;
    public float stayTime;
    public float backgroundAlpha;

    string messageBacklog;
    float fadeOutTimer = -200f;

    [ContextMenu("Test Monologue")]
    public void MessageTest()
    {
        StartMessage("I got bitches, all on my dick, each and every day.");
    }

    public void StartMessage(string message)
    {
        textElement.gameObject.SetActive(true);

        textElement.color = Color.white;
        messageBacklog = message;

        StartCoroutine(TypeWrite());
    }

    IEnumerator TypeWrite()
    {
        foreach(char c in messageBacklog)
        {
            textElement.text += c;
            yield return new WaitForSeconds(timeBetweenLetters);
        }
        fadeOutTimer = fadeTime;
    }

    void Update()
    {
        if(fadeOutTimer + stayTime > 0.0f)
        {
            textElement.color = Color.Lerp(Color.white, new Color(1f, 1f, 1f, 0f), fadeTime-(fadeOutTimer + stayTime));
            fadeOutTimer -= Time.deltaTime;
            if(fadeOutTimer + stayTime < 0.0f)
            {
                textElement.text = "";
                textElement.gameObject.SetActive(false);
            }
        }
    }

}
