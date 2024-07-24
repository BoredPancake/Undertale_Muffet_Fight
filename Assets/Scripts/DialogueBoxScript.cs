using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxScript : MainScript
{
    public string currentQuote;
    public string currentText;
    public bool finishedSpeaking;

    public GameObject muffetTextObj;
    public GameObject muffetDialogueBox;
    public Text flavorText;
    TMP_Text muffetText;
    
    public IEnumerator writing;

    AudioClip charaVoice;
    AudioSource audioSource;
    IEnumerator Start()
    {
        flavorText.text = "";
        audioSource = GetComponent<AudioSource>();
        muffetText = muffetTextObj.GetComponent<TMP_Text>();
        charaVoice = audioSource.clip;
        currentText = "* Muffet traps you!";
        currentQuote = "Don't look so blue, my deary~";
        yield return new WaitForEndOfFrame();
        writing = Write(currentText);
        StartCoroutine(writing);
    }

    // Update is called once per frame
    void Update()
    {
        if (turn == "Player")
        {
            flavorText.enabled = true;
        }
        else{
            flavorText.enabled = false;
        }
    }

    public void StopWriting()
    {
        StopCoroutine(writing);
        flavorText.text = "";
    }

    public IEnumerator Write(string text, bool clear = true)
    {
        audioSource.clip = charaVoice;
        if (clear){
            flavorText.text = "";
        }
        yield return new WaitForEndOfFrame();
        
        bool selecting = player.GetComponent<ActionScript>().selecting;
        
        foreach (char character in text)
        {
            if (Input.GetKeyDown("z") && selecting)
            {
                yield break;
            }
            if (character != ' ')
            {
                audioSource.Play();
                yield return new WaitForSeconds(0.04f);
            }
            flavorText.text += character;
        }
    }

    
    public IEnumerator Say(string text, bool canSkip = false) // Same as write, but for Muffet
    {
        finishedSpeaking = false;
        muffetDialogueBox.SetActive(true);
        audioSource.clip = audioClip1;
        muffetText.text = "";
        foreach (char character in text)
        {
            muffetText.text += character;
            if (character != ' ')
            {
                audioSource.Play();
                if (character == ',' || character == '.' || character == '!') yield return new WaitForSeconds(0.1f);
                yield return new WaitForSeconds(0.04f);
            }
            if(Input.GetKeyDown("x") && canSkip){
                flavorText.text = text;
                break;
            }
        }
        while(!(Input.GetKeyDown("z") || Input.GetKeyDown("x")))
        {
            yield return null;
        }
        finishedSpeaking = true;
        muffetDialogueBox.SetActive(false); // The text itself is black and won't be visible without the box

    }

    public IEnumerator ChangeScale(int direction)
    {
        if (direction == 0)
        {
            while(transform.localScale.x > 0.4f)
            {
                transform.localScale -= new Vector3(0.05f, 0);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while(transform.localScale.x < 1)
            {
                transform.localScale += new Vector3(0.05f, 0);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
